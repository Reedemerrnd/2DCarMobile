using BattleScripts;
using Game.Controllers;
using Game.Models;
using Game.Utils;
using UnityEngine;

namespace Game.Fight
{
    internal class FightController : BaseController
    {
        private readonly IGameModel _gameModel;
        private readonly IResourceLoader _resourceLoader;
        private FightUIView _fightUIView;

        private int _allCountMoneyPlayer;
        private int _allCountHealthPlayer;
        private int _allCountPowerPlayer;
        private int _allCountWantedPlayer;

        private DataPlayer _money;
        private DataPlayer _heath;
        private DataPlayer _power;
        private DataPlayer _wanted;

        private Enemy _enemy;


        public FightController(IGameModel gameModel, IResourceLoader resourceLoader)
        {
            _gameModel = gameModel;
            _resourceLoader = resourceLoader;
            Init();
        }

        private void Init()
        {
            _enemy = new Enemy("Enemy Car");

            _fightUIView = _resourceLoader.Spawn<FightUIView>(UIType.Fight, Vector3.zero, Quaternion.identity);
            AddGameObject(_fightUIView.gameObject);
            
            _money = CreateDataPlayer(DataType.Money);
            _heath = CreateDataPlayer(DataType.Health);
            _power = CreateDataPlayer(DataType.Power);
            _wanted = CreateDataPlayer(DataType.Wanted);

            Subscribe();
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
            _fightUIView.AddMoneyButton.onClick.AddListener(IncreaseMoney);
            _fightUIView.MinusMoneyButton.onClick.AddListener(DecreaseMoney);

            _fightUIView.AddHealthButton.onClick.AddListener(IncreaseHealth);
            _fightUIView.MinusHealthButton.onClick.AddListener(DecreaseHealth);

            _fightUIView.AddPowerButton.onClick.AddListener(IncreasePower);
            _fightUIView.MinusPowerButton.onClick.AddListener(DecreasePower);

            _fightUIView.AddWantedButton.onClick.AddListener(IncreaseWanted);
            _fightUIView.MinusWantedButton.onClick.AddListener(DecreaseWanted);
            
             _fightUIView.SkipButton.onClick.AddListener(ExitFightHandler);
             _fightUIView.FightButton.onClick.AddListener(FightHandler);
        }

        private void Unsubscribe()
        {
            _fightUIView.AddMoneyButton.onClick.RemoveListener(IncreaseMoney);
            _fightUIView.MinusMoneyButton.onClick.RemoveListener(DecreaseMoney);

            _fightUIView.AddHealthButton.onClick.RemoveListener(IncreaseHealth);
            _fightUIView.MinusHealthButton.onClick.RemoveListener(DecreaseHealth);

            _fightUIView.AddPowerButton.onClick.RemoveListener(IncreasePower);
            _fightUIView.MinusPowerButton.onClick.RemoveListener(DecreasePower);

            _fightUIView.AddWantedButton.onClick.RemoveListener(IncreaseWanted);
            _fightUIView.MinusWantedButton.onClick.RemoveListener(DecreaseWanted);
            
            _fightUIView.SkipButton.onClick.RemoveListener(ExitFightHandler);
            _fightUIView.FightButton.onClick.AddListener(FightHandler);

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
            _fightUIView.ChangePlayerDataWindow(value, dataType);
        }

        private void FightHandler()
        {
            int enemyPower = _enemy.CalcPower();
            bool isVictory = _allCountPowerPlayer >= enemyPower;
            if (isVictory)
            {
                _gameModel.UpdateState(GameState.RunGame);
            }
            else
            {
                _gameModel.UpdateState(GameState.MainMenu);
            }
        }


        private void ExitFightHandler() => _gameModel.UpdateState(GameState.RunGame);

        protected override void OnDispose()
        {
            DisposeDataPlayer(ref _money);
            DisposeDataPlayer(ref _heath);
            DisposeDataPlayer(ref _power);
            DisposeDataPlayer(ref _wanted);

            Unsubscribe();

            base.OnDispose();
        }
    }
}