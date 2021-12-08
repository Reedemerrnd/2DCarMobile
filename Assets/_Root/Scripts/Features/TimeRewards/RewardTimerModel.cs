using System;

namespace Rewards
{
    internal class RewardTimerModel
    {
        private const float DAY_IN_SECONDS = 86400;
        private const float DAY_DEADLINE_IN_SECONDS = DAY_IN_SECONDS * 2;

        private const float WEEK_IN_SECONDS = 604800;
        private const float WEEK_DEADLINE_IN_SECONDS = WEEK_IN_SECONDS * 3;

        private DateTime? _lastDailyClaimTime;
        private DateTime? _lastWeeklyClaimTime;

        public event Action OnDailyTimerReset;
        public event Action OnWeeklyTimerReset; 
        
        public RewardTimerModel()
        {
            
        }
        
        private bool CanGetReward(RewardDelayType delayType)
        {
            return false;
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
    }
}