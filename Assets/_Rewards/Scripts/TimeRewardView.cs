using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rewards
{
    internal class TimeRewardView : MonoBehaviour
    {
        private const string CurrentSlotInActiveKey = nameof(CurrentSlotInActiveKey);
        private const string TimeGetRewardKey = nameof(TimeGetRewardKey);

        private const float DayInSeconds = 86400;
        private const float DayDeadlineInSeconds = DayInSeconds * 2;

        private const float WeekInSeconds = 604800;
        private const float WeekDeadlineInSeconds = WeekInSeconds * 3;

        [field: Header("Settings Time Get Reward")]
        [field: SerializeField] public RewardDelayType RewardDelay { get; private set; }
        public float TimeCooldown => RewardDelay switch
        {
            RewardDelayType.Daily => DayInSeconds,
            RewardDelayType.Weekly => WeekInSeconds,
            _ => DayInSeconds
        };
        public float TimeDeadline => RewardDelay switch
        {
            RewardDelayType.Daily => DayDeadlineInSeconds,
            RewardDelayType.Weekly => WeekDeadlineInSeconds,
            _ => DayDeadlineInSeconds
        };

        [field: Header("Settings Rewards")]
        [field: SerializeField] public List<Reward> Rewards { get; private set; }

        [field: Header("Ui Elements")]
        [field: SerializeField] public TMP_Text TimerNewReward { get; private set; }
        [field: SerializeField] public Transform MountRootSlotsReward { get; private set; }
        [field: SerializeField] public ContainerSlotRewardView ContainerSlotRewardPrefab { get; private set; }
        [field: SerializeField] public Button GetRewardButton { get; private set; }
        [field: SerializeField] public Button ResetButton { get; private set; }

        public int CurrentSlotInActive
        {
            get => PlayerPrefs.GetInt(CurrentSlotInActiveKey, 0);
            set => PlayerPrefs.SetInt(CurrentSlotInActiveKey, value);
        }

        public DateTime? TimeGetReward
        {
            get
            {
                string data = PlayerPrefs.GetString(TimeGetRewardKey, null);
                return !string.IsNullOrEmpty(data) ? (DateTime?)DateTime.Parse(data) : null;
            }
            set
            {
                if (value != null)
                    PlayerPrefs.SetString(TimeGetRewardKey, value.ToString());
                else
                    PlayerPrefs.DeleteKey(TimeGetRewardKey);
            }
        }
    }
}