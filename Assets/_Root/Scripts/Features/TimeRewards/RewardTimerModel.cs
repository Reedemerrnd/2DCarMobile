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

        private DateTime? _lastDailyClaimTime;
        private DateTime? _lastWeeklyClaimTime;

        private const string LAST_DAILY_CLAIM_TIME_KEY = nameof(_lastDailyClaimTime);
        private const string LAST_WEEKLY_CLAIM_TIME_KEY = nameof(_lastWeeklyClaimTime);

        public event Action OnDailyTimerReset;
        public event Action OnWeeklyTimerReset; 
        
        public RewardTimerModel()
        {
            LoadClaimTimeStamps();
        }

        private void LoadClaimTimeStamps()
        {
            var data = PlayerPrefs.GetString(LAST_DAILY_CLAIM_TIME_KEY, null);
            _lastDailyClaimTime = !string.IsNullOrEmpty(data) ? (DateTime?) DateTime.Parse(data) : null;
            
            data = PlayerPrefs.GetString(LAST_WEEKLY_CLAIM_TIME_KEY, null);
            _lastWeeklyClaimTime = !string.IsNullOrEmpty(data) ? (DateTime?) DateTime.Parse(data) : null;
        }

        private bool TryGetReward(RewardDelayType delayType) => delayType switch
        {
            RewardDelayType.Daily => TryClaimDaily(),
            RewardDelayType.Weekly => TryClaimWeekly(),
            _ => false
        };

        private bool TryClaimDaily()
        {
            var passed = CheckDailyTimer();
            if(passed)
                _lastDailyClaimTime = DateTime.UtcNow;
            return passed;
        }
        
        private bool TryClaimWeekly()
        {
            var passed = CheckWeeklyTimer();
            if(passed)
                _lastWeeklyClaimTime = DateTime.UtcNow;
            return passed;
        }

        private bool CheckDailyTimer()
        {
            if(_lastDailyClaimTime.HasValue)
            {
                return true;
            }
            
            TimeSpan timeFromLastRewardGetting = DateTime.UtcNow - _lastDailyClaimTime.Value;
            
            HandleDailyDeadline(timeFromLastRewardGetting);
            return timeFromLastRewardGetting.Seconds >= DAY_IN_SECONDS;
        }

        private void HandleDailyDeadline(TimeSpan timeFromLastRewardGetting)
        {
            var isDeadlineExpired = timeFromLastRewardGetting.Seconds >= DAY_DEADLINE_IN_SECONDS;
            if (isDeadlineExpired)
            {
                _lastDailyClaimTime = null;
                OnDailyTimerReset?.Invoke();
            }
        }

        private bool CheckWeeklyTimer()
        {
            if(_lastWeeklyClaimTime.HasValue)
            {
                return true;
            }
            
            TimeSpan timeFromLastRewardGetting = DateTime.UtcNow - _lastWeeklyClaimTime.Value;
            HandleWeeklyDeadline(timeFromLastRewardGetting);
            return timeFromLastRewardGetting.Seconds >= WEEK_IN_SECONDS;
        }

        private void HandleWeeklyDeadline(TimeSpan timeFromLastRewardGetting)
        {
            var isDeadlineExpired = timeFromLastRewardGetting.Seconds >= WEEK_DEADLINE_IN_SECONDS;
            if (isDeadlineExpired)
            {
                _lastWeeklyClaimTime = null;
                OnWeeklyTimerReset?.Invoke();
            }
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
            SetPrefsValue(LAST_DAILY_CLAIM_TIME_KEY, _lastDailyClaimTime.ToString());
            SetPrefsValue(LAST_WEEKLY_CLAIM_TIME_KEY, _lastWeeklyClaimTime.ToString());
        }
    }
}