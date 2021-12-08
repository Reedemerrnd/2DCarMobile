using System;
using Game.Utils;
using Rewards;
using UnityEngine;

namespace Game
{
    internal class PlayerPrefsCurrencyModel : IDisposable, ICurrencyModel
    {
        private SubscriptionProperty<int> _wood;
        private SubscriptionProperty<int> _diamond;

        public IReadOnlySubscriptionProperty<int> Wood => _wood;
        public IReadOnlySubscriptionProperty<int> Diamond => _diamond;
        
        
        public PlayerPrefsCurrencyModel()
        {
            var wood = PlayerPrefs.GetInt(CurrencyType.Wood.ToString());
            var diamond = PlayerPrefs.GetInt(CurrencyType.Diamond.ToString());
            
            _wood = new SubscriptionProperty<int>(wood);
            _diamond = new SubscriptionProperty<int>(diamond);
        }

        public void Dispose()
        {
            PlayerPrefs.SetInt(CurrencyType.Wood.ToString(), _wood.Value);
            PlayerPrefs.SetInt(CurrencyType.Diamond.ToString(), _diamond.Value);
            PlayerPrefs.Save();
        }
    }
}