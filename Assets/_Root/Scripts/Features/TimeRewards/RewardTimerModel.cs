using System;
using UnityEngine;

namespace Rewards
{
    internal class RewardTimerModel : IDisposable
    {
        private const float DAY_IN_SECONDS = 86400;
        private const float DAY_DEADLINE_IN_SECONDS = DAY_IN_SECONDS * 2;

        private const float WEEK_IN_SECONDS = 604800;
        private const float WEEK_DEADLINE_IN_SECONDS = WEEK_IN_SECONDS * 3;
        
        private const string CURRENT_SLOT_IN_ACTIVE_KEY = nameof(CURRENT_SLOT_IN_ACTIVE_KEY);

        private RewardDelayType _delayType;

        private float _rewardCooldown;
        private float _deadlineDelay;
        
        private DateTime? _lastClaimTime;
        
        public DateTime? LastClaimTime => _lastClaimTime;
        public float RewardCooldown => _rewardCooldown;
        public RewardDelayType DelayType => _delayType;
        public int CurrentSlotInActive
        {
            get => PlayerPrefs.GetInt(CURRENT_SLOT_IN_ACTIVE_KEY, 0);
            set => PlayerPrefs.SetInt(CURRENT_SLOT_IN_ACTIVE_KEY, value);
        }

        public RewardTimerModel(RewardDelayType delayType)
        {
            _delayType = delayType;
            SwitchDelayValues();
            LoadClaimTimeStamps();
        }

        private void SwitchDelayValues()
        {
            _rewardCooldown = _delayType switch
            {
                RewardDelayType.Daily => DAY_IN_SECONDS,
                RewardDelayType.Weekly => WEEK_IN_SECONDS,
                _ => DAY_IN_SECONDS
            };
            _deadlineDelay = _delayType switch
            {
                RewardDelayType.Daily => DAY_DEADLINE_IN_SECONDS,
                RewardDelayType.Weekly => WEEK_DEADLINE_IN_SECONDS,
                _ => DAY_DEADLINE_IN_SECONDS
            };
        }
        
        private void LoadClaimTimeStamps()
        {
            var data = PlayerPrefs.GetString(_delayType.ToString(), null);
            _lastClaimTime = !string.IsNullOrEmpty(data) ? (DateTime?) DateTime.Parse(data) : null;
        }

        public bool ClaimReward()
        {
            var passed = IsTimerReady();
            if(passed)
                _lastClaimTime = DateTime.UtcNow;
            HandleDailyDeadline();
            return passed;
        }

        public bool IsTimerReady()
        {
            if(!_lastClaimTime.HasValue)
            {
                return true;
            }
            
            TimeSpan timeFromLastRewardGetting = DateTime.UtcNow - _lastClaimTime.Value;
            
            
            return timeFromLastRewardGetting.Seconds >= _rewardCooldown;
        }

        private void HandleDailyDeadline()
        {
            TimeSpan timeFromLastRewardGetting = DateTime.UtcNow - _lastClaimTime.Value;
            var isDeadlineExpired = timeFromLastRewardGetting.Seconds >= _deadlineDelay;
            if (isDeadlineExpired)
            {
                _lastClaimTime = null;
            }
        }

        public void ResetAll()
        {
            SetPrefsValue(_delayType.ToString(), null);
            SetPrefsValue(CURRENT_SLOT_IN_ACTIVE_KEY, 0.ToString());
            LoadClaimTimeStamps();
        }
        
        private void SetPrefsValue(string key, string value)
        {
            if (value != null)
                PlayerPrefs.SetString(key, value);
            else
                PlayerPrefs.DeleteKey(key);
        }
        
        public void Dispose()
        {
            SetPrefsValue(_delayType.ToString(), _lastClaimTime.ToString());
        }
    }
}