using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Rewards
{
    internal class TImeRewardController
    {
        private readonly TimeRewardView _timeRewardView;

        private List<ContainerSlotRewardView> _slots;
        private Coroutine _coroutine;

        private bool _isGetReward;
        private bool _isInitialized;


        public TImeRewardController(TimeRewardView generateLevelView)
        {
            _timeRewardView = generateLevelView;
        }


        public void InitView()
        {
            if (_isInitialized)
                return;

            InitSlots();
            RefreshUi();
            StartRewardsUpdating();
            SubscribeButtons();

            _isInitialized = true;
        }

        public void Deinit()
        {
            if (!_isInitialized)
                return;

            DeinitSlots();
            StopRewardsUpdating();
            UnsubscribeButtons();

            _isInitialized = false;
        }


        private void InitSlots()
        {
            _slots = new List<ContainerSlotRewardView>();

            for (int i = 0; i < _timeRewardView.Rewards.Count; i++)
            {
                ContainerSlotRewardView instanceSlot = CreateSlotRewardView();
                _slots.Add(instanceSlot);
            }
        }

        private ContainerSlotRewardView CreateSlotRewardView() =>
            Object.Instantiate(_timeRewardView.ContainerSlotRewardPrefab, _timeRewardView.MountRootSlotsReward, false);

        private void DeinitSlots()
        {
            foreach (ContainerSlotRewardView slot in _slots)
                Object.Destroy(slot.gameObject);

            _slots.Clear();
        }


        private void StartRewardsUpdating() =>
            _coroutine = _timeRewardView.StartCoroutine(RewardsStateUpdater());

        private void StopRewardsUpdating()
        {
            if (_coroutine == null)
                return;

            _timeRewardView.StopCoroutine(_coroutine);
            _coroutine = null;
        }

        private IEnumerator RewardsStateUpdater()
        {
            WaitForSeconds waitForSecond = new WaitForSeconds(1);

            while (true)
            {
                RefreshRewardsState();
                RefreshUi();
                yield return waitForSecond;
            }
        }


        private void RefreshRewardsState()
        {
            bool gotRewardEarlier = _timeRewardView.TimeGetReward.HasValue;
            if (!gotRewardEarlier)
            {
                _isGetReward = true;
                return;
            }

            TimeSpan timeFromLastRewardGetting = DateTime.UtcNow - _timeRewardView.TimeGetReward.Value;
            bool isDeadlineElapsed = timeFromLastRewardGetting.Seconds >= _timeRewardView.TimeDeadline;
            bool isTimeToGetNewReward = timeFromLastRewardGetting.Seconds >= _timeRewardView.TimeCooldown;

            if (isDeadlineElapsed)
                ResetRewardsState();

            _isGetReward = isTimeToGetNewReward;
        }

        private void ResetRewardsState()
        {
            _timeRewardView.TimeGetReward = null;
            _timeRewardView.CurrentSlotInActive = 0;
        }


        private void RefreshUi()
        {
            _timeRewardView.GetRewardButton.interactable = _isGetReward;
            _timeRewardView.TimerNewReward.text = GetTimerNewRewardText();
            RefreshSlots();
        }

        private string GetTimerNewRewardText()
        {
            if (_isGetReward)
                return "The reward is ready to be received!";

            if (_timeRewardView.TimeGetReward.HasValue)
            {
                DateTime nextClaimTime = _timeRewardView.TimeGetReward.Value.AddSeconds(_timeRewardView.TimeCooldown);
                TimeSpan currentClaimCooldown = nextClaimTime - DateTime.UtcNow;
                string timeGetReward = $"{currentClaimCooldown.Days:D2}:{currentClaimCooldown.Hours:D2}:" +
                                       $"{currentClaimCooldown.Minutes:D2}:{currentClaimCooldown.Seconds:D2}";

                return $"Time to get the next reward: {timeGetReward}";
            }

            return string.Empty;
        }

        private void RefreshSlots()
        {
            for (var i = 0; i < _slots.Count; i++)
            {
                Reward reward = _timeRewardView.Rewards[i];
                int count = i + 1;
                bool isSelect = i == _timeRewardView.CurrentSlotInActive;

                _slots[i].SetData(reward,_timeRewardView.RewardDelay, count, isSelect);
            }
        }


        private void SubscribeButtons()
        {
            _timeRewardView.GetRewardButton.onClick.AddListener(ClaimReward);
            _timeRewardView.ResetButton.onClick.AddListener(ResetTimer);
        }

        private void UnsubscribeButtons()
        {
            _timeRewardView.GetRewardButton.onClick.RemoveListener(ClaimReward);
            _timeRewardView.ResetButton.onClick.RemoveListener(ResetTimer);
        }

        private void ClaimReward()
        {
            if (!_isGetReward)
                return;

            Reward reward = _timeRewardView.Rewards[_timeRewardView.CurrentSlotInActive];

            switch (reward.CurrencyType)
            {
                case CurrencyType.Wood:
                    CurrencyView.Instance.AddWood(reward.CountCurrency);
                    break;
                case CurrencyType.Diamond:
                    CurrencyView.Instance.AddDiamond(reward.CountCurrency);
                    break;
            }

            _timeRewardView.TimeGetReward = DateTime.UtcNow;
            _timeRewardView.CurrentSlotInActive++;

            RefreshRewardsState();
        }

        private void ResetTimer()
        {
            PlayerPrefs.DeleteAll();
            CurrencyView.Instance.RefreshText();
        }
    }
}