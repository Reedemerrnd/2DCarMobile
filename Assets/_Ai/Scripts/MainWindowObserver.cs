using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BattleScripts
{
    public class MainWindowObserver : MonoBehaviour
    {
        [Header("Player Stats")]
        [SerializeField] private TMP_Text _countMoneyText;
        [SerializeField] private TMP_Text _countHealthText;
        [SerializeField] private TMP_Text _countPowerText;
        [SerializeField] private TMP_Text _countWantedText;

        [Header("Enemy Stats")]
        [SerializeField] private TMP_Text _countPowerEnemyText;

        [Header("Money Buttons")]
        [SerializeField] private Button _addMoneyButton;
        [SerializeField] private Button _minusMoneyButton;

        [Header("Health Buttons")]
        [SerializeField] private Button _addHealthButton;
        [SerializeField] private Button _minusHealthButton;

        [Header("Power Buttons")]
        [SerializeField] private Button _addPowerButton;
        [SerializeField] private Button _minusPowerButton;

        [Header("Wanted Buttons")]
        [SerializeField] private Button _addWantedButton;
        [SerializeField] private Button _minusWantedButton;

        [Header("Other Buttons")]
        [SerializeField] private Button _fightButton;
        [SerializeField] private Button _skipButton;

        private int _allCountMoneyPlayer;
        private int _allCountHealthPlayer;
        private int _allCountPowerPlayer;
        private int _allCountWantedPlayer;

        private DataPlayer _money;
        private DataPlayer _heath;
        private DataPlayer _power;
        private DataPlayer _wanted;

        private Enemy _enemy;


        private void Start()
        {
            _enemy = new Enemy("Enemy Flappy");

            _money = CreateDataPlayer(DataType.Money);
            _heath = CreateDataPlayer(DataType.Health);
            _power = CreateDataPlayer(DataType.Power);
            _wanted = CreateDataPlayer(DataType.Wanted);

            Subscribe();
        }

        private void OnDestroy()
        {
            DisposeDataPlayer(ref _money);
            DisposeDataPlayer(ref _heath);
            DisposeDataPlayer(ref _power);
            DisposeDataPlayer(ref _wanted);

            Unsubscribe();
        }


        private DataPlayer CreateDataPlayer(DataType dataType)
        {
            DataPlayer dataPlayer = new DataPlayer(dataType);
            dataPlayer.Attach(_enemy);

            return dataPlayer;
        }

        private void DisposeDataPlayer(ref DataPlayer dataPlayer)
        {
            dataPlayer.Detach(_enemy);
            dataPlayer = null;
        }


        private void Subscribe()
        {
            _addMoneyButton.onClick.AddListener(IncreaseMoney);
            _minusMoneyButton.onClick.AddListener(DecreaseMoney);

            _addHealthButton.onClick.AddListener(IncreaseHealth);
            _minusHealthButton.onClick.AddListener(DecreaseHealth);

            _addPowerButton.onClick.AddListener(IncreasePower);
            _minusPowerButton.onClick.AddListener(DecreasePower);

            _addWantedButton.onClick.AddListener(IncreaseWanted);
            _minusWantedButton.onClick.AddListener(DecreaseWanted);

            _fightButton.onClick.AddListener(Fight);
            _skipButton.onClick.AddListener(Skip);
        }

        private void Unsubscribe()
        {
            _addMoneyButton.onClick.RemoveAllListeners();
            _minusMoneyButton.onClick.RemoveAllListeners();

            _addHealthButton.onClick.RemoveAllListeners();
            _minusHealthButton.onClick.RemoveAllListeners();

            _addPowerButton.onClick.RemoveAllListeners();
            _minusPowerButton.onClick.RemoveAllListeners();

            _addWantedButton.onClick.RemoveAllListeners();
            _minusWantedButton.onClick.RemoveAllListeners();

            _fightButton.onClick.RemoveAllListeners();
            _skipButton.onClick.RemoveAllListeners();
        }


        private void IncreaseMoney() => IncreaseValue(ref _allCountMoneyPlayer, DataType.Money);
        private void DecreaseMoney() => DecreaseValue(ref _allCountMoneyPlayer, DataType.Money);

        private void IncreaseHealth() => IncreaseValue(ref _allCountHealthPlayer, DataType.Health);
        private void DecreaseHealth() => DecreaseValue(ref _allCountHealthPlayer, DataType.Health);

        private void IncreasePower() => IncreaseValue(ref _allCountPowerPlayer, DataType.Power);
        private void DecreasePower() => DecreaseValue(ref _allCountPowerPlayer, DataType.Power);

        private void IncreaseWanted() => IncreaseValue(ref _allCountWantedPlayer, DataType.Wanted);
        private void DecreaseWanted() => DecreaseValue(ref _allCountWantedPlayer, DataType.Wanted);

        private void IncreaseValue(ref int value, DataType dataType) => AddToValue(ref value, 1, dataType);
        private void DecreaseValue(ref int value, DataType dataType) => AddToValue(ref value, -1, dataType);

        private void AddToValue(ref int value, int addition, DataType dataType)
        {
            value += addition;
            ChangeDataWindow(value, dataType);
        }


        private void ChangeDataWindow(int countChangeData, DataType dataType)
        {
            DataPlayer dataPlayer = GetDataPlayer(dataType);
            TMP_Text textComponent = GetTextComponent(dataType);
            string text = $"Player {dataType:F} {countChangeData}";

            dataPlayer.Value = countChangeData;
            textComponent.text = text;

            int enemyPower = _enemy.CalcPower();
            _countPowerEnemyText.text = $"Enemy Power {enemyPower}";

            ActivateSkipButton();
        }

        private void ActivateSkipButton()
        {
            if(_allCountWantedPlayer <= 2)
            {
                _skipButton.interactable = true;
            }
            else
            {
                _skipButton.interactable = false;
            }
        }

        private TMP_Text GetTextComponent(DataType dataType) =>
            dataType switch
            {
                DataType.Money => _countMoneyText,
                DataType.Health => _countHealthText,
                DataType.Power => _countPowerText,
                DataType.Wanted => _countWantedText,
                _ => throw new ArgumentException($"Wrong {nameof(DataType)}")
            };

        private DataPlayer GetDataPlayer(DataType dataType) =>
            dataType switch
            {
                DataType.Money => _money,
                DataType.Health => _heath,
                DataType.Power => _power,
                DataType.Wanted => _wanted,
                _ => throw new ArgumentException($"Wrong {nameof(DataType)}")
            };


        private void Skip()
        {
            Debug.Log($"<color=07FF00>Skipped!!!</color>");
        }

        private void Fight()
        {
            int enemyPower = _enemy.CalcPower();
            bool isVictory = _allCountPowerPlayer >= enemyPower;

            string color = isVictory ? "#07FF00" : "#FF0000";
            string message = isVictory ? "Win" : "Lose";

            Debug.Log($"<color={color}>{message}!!!</color>");
        }
    }
}