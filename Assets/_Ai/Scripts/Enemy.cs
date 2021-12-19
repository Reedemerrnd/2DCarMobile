using UnityEngine;

namespace BattleScripts
{
    internal class Enemy : IEnemy
    {
        private const float _kMoney = 5f;
        private const float _kPower = 1.5f;
        private const float _maxHealthPlayer = 20;

        private readonly string _name;

        private int _moneyPlayer;
        private int _healthPlayer;
        private int _powerPlayer;
        private int _wantedPlayer;

        public Enemy(string name) =>
            _name = name;


        public void Update(DataPlayer dataPlayer)
        {
            switch (dataPlayer.DataType)
            {
                case DataType.Money:
                    _moneyPlayer = dataPlayer.Value;
                    break;

                case DataType.Health:
                    _healthPlayer = dataPlayer.Value;
                    break;

                case DataType.Power:
                    _powerPlayer = dataPlayer.Value;
                    break;

                case DataType.Wanted:
                    _wantedPlayer = dataPlayer.Value;
                    break;
            }

            Debug.Log($"Notified {_name} change to {dataPlayer}");
        }

        public int CalcPower()
        {
            int kHealth = CalcKHealth();
            float moneyRatio = _moneyPlayer / _kMoney;
            float powerRatio = _powerPlayer / _kPower;
            float wantedRatio = moneyRatio * _wantedPlayer;

            return (int)(moneyRatio + kHealth + powerRatio + wantedRatio);
        }

        private int CalcKHealth() =>
            _healthPlayer > _maxHealthPlayer ? 100 : 5;
    }
}
