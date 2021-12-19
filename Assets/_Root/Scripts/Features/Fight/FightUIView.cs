using System;
using BattleScripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Fight
{
    internal class FightUIView : MonoBehaviour
    {
        [field: Header("Player Stats")] 
        [field: SerializeField] public TMP_Text CountMoneyText { get; private set; }
        [field: SerializeField] public TMP_Text CountHealthText { get; private set; }
        [field: SerializeField] public TMP_Text CountPowerText { get; private set; }
        [field: SerializeField] public TMP_Text CountWantedText { get; private set; }

        [field: Header("Enemy Stats")]
        [field: SerializeField] public TMP_Text CountPowerEnemyText { get; private set; }

        [field: Header("Money Buttons")]
        [field: SerializeField] public Button AddMoneyButton { get; private set; }
        [field: SerializeField] public Button MinusMoneyButton { get; private set; }

        [field: Header("Health Buttons")]
        [field: SerializeField] public Button AddHealthButton { get; private set; }
        [field: SerializeField] public Button MinusHealthButton { get; private set; }

        [field: Header("Power Buttons")]
        [field: SerializeField] public Button AddPowerButton { get; private set; }
        [field: SerializeField] public Button MinusPowerButton { get; private set; }

        [field: Header("Wanted Buttons")]
        [field: SerializeField] public Button AddWantedButton { get; private set; }
        [field: SerializeField] public Button MinusWantedButton { get; private set; }

        [field: Header("Other Buttons")]
        [field: SerializeField] public Button FightButton { get; private set; }
        [field: SerializeField] public Button SkipButton { get; private set; }

        public void ChangePlayerDataWindow(int countChangeData, DataType dataType)
        {
            TMP_Text textComponent = GetTextComponent(dataType);
            string text = $"Player {dataType:F} {countChangeData}";

            textComponent.text = text;
        }


        public void ChangeEnemyDataWindow(int enemyPower)
        {
            CountPowerEnemyText.text = $"Enemy Power {enemyPower}";
        }
        
        private TMP_Text GetTextComponent(DataType dataType) =>
            dataType switch
            {
                DataType.Money => CountMoneyText,
                DataType.Health => CountHealthText,
                DataType.Power => CountPowerText,
                DataType.Wanted => CountWantedText,
                _ => throw new ArgumentException($"Wrong {nameof(DataType)}")
            };

    }
}