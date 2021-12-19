using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using Game.Controllers;
using Game.Models;
using Game.Utils;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Rewards
{
    internal class TimeRewardController : BaseController
    {
        private TimeRewardsVIew _timeRewardView;
        private CurrenciesView _currencyView;
        
        private readonly IGameModel _gameModel;
        private readonly IResourceLoader _resourceLoader;
        private RewardTimerModel _rewardTimerModel;

        private ICurrencyModel _currencyModel;
        private List<ContainerSlotRewardView> _slots;
        private Coroutine _coroutine;

        private bool _isGetReward;
        private bool _isInitialized;


        public TimeRewardController(IGameModel gameModel, IResourceLoader resourceLoader, RewardDelayType rewardDelayType)
        {
            _gameModel = gameModel;
            _resourceLoader = resourceLoader;
            _currencyModel = gameModel.Currencies;

            _rewardTimerModel = new RewardTimerModel(rewardDelayType);
            

            
            LoadViews();
            InitViews();
        }

        private void OpenMainMenu()
        {
            _gameModel.UpdateState(GameState.MainMenu);
        }

        private void InitViews()
        {
            if (_isInitialized)
                return;
            
            InitCurrencyView();
            InitSlots();
            RefreshUi();
            StartRewardsUpdating();
            SubscribeButtons();

            _isInitialized = true;
        }

        private void InitCurrencyView()
        {
            _currencyModel.Diamond.SubscribeOnChange(_currencyView.SetDiamond);
            _currencyModel.Wood.SubscribeOnChange(_currencyView.SetWood);
            ForceRefreshCurrencies();
        }
        private void DeInitCurrencyView()
        {
            _currencyModel.Diamond.UnSubscribeOnChange(_currencyView.SetDiamond);
            _currencyModel.Wood.UnSubscribeOnChange(_currencyView.SetWood);
        }

        private void LoadViews()
        {
            _timeRewardView = _resourceLoader.Spawn<TimeRewardsVIew>(UIType.Rewards, Vector3.zero, Quaternion.identity);
            AddGameObject(_timeRewardView.gameObject);

            _currencyView = _resourceLoader.Spawn<CurrenciesView>(UIType.Currency, Vector3.zero, Quaternion.identity);
            AddGameObject(_currencyView.gameObject);
        }
        
        public void Deinit()
        {
            if (!_isInitialized)
                return;
            DeInitCurrencyView();
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
                _isGetReward = _rewardTimerModel.IsTimerReady();
                RefreshUi();
                yield return waitForSecond;
            }
        }

        private void ForceRefreshCurrencies()
        {
            _currencyView.SetDiamond(_currencyModel.Diamond.Value);
            _currencyView.SetWood(_currencyModel.Wood.Value);
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

            if (_rewardTimerModel.LastClaimTime.HasValue)
            {
                DateTime nextClaimTime = _rewardTimerModel.LastClaimTime.Value.AddSeconds(_rewardTimerModel.RewardCooldown);
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
                bool isSelect = i == _rewardTimerModel.CurrentSlotInActive;

                _slots[i].SetData(reward, _rewardTimerModel.DelayType, count, isSelect);
            }
        }


        private void SubscribeButtons()
        {
            _timeRewardView.GetRewardButton.onClick.AddListener(ClaimReward);
            _timeRewardView.ResetButton.onClick.AddListener(ResetTimer);
            _timeRewardView.BackButton.onClick.AddListener(OpenMainMenu);
        }

        private void UnsubscribeButtons()
        {
            _timeRewardView.GetRewardButton.onClick.RemoveListener(ClaimReward);
            _timeRewardView.ResetButton.onClick.RemoveListener(ResetTimer);
            _timeRewardView.BackButton.onClick.RemoveListener(OpenMainMenu);
        }

        private void ClaimReward()
        {
            if (!_isGetReward)
                return;

            Reward reward = _timeRewardView.Rewards[_rewardTimerModel.CurrentSlotInActive];
            
            _currencyModel.SetCurrency(reward.CurrencyType, reward.CountCurrency);

            _rewardTimerModel.ClaimReward();
            _rewardTimerModel.CurrentSlotInActive++;
        }

        private void ResetTimer()
        {
            _rewardTimerModel.ResetAll();
            _currencyModel.Reset();
            ForceRefreshCurrencies();
            RefreshUi();
        }

        protected override void OnDispose()
        {
            _rewardTimerModel.Dispose();
            _currencyModel.Save();
            Deinit();
            base.OnDispose();
        }
    }
}