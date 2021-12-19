
using UnityEngine;

namespace Rewards
{
    public class CurrenciesView : MonoBehaviour
    {
        [SerializeField] private CurrencySlotView _currencyWood;
        [SerializeField] private CurrencySlotView _currentDiamond;
        
        public void SetWood(int value)
        {
            _currencyWood.SetData(value);
        }

        public void SetDiamond(int value)
        {
            _currentDiamond.SetData(value);
        }
    }
}