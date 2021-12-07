using Game.Abilities;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    internal class InGameUIView : MonoBehaviour, IInGameUIView
    {
        [SerializeField] private Button _activeAbilityButton;
        [SerializeField] private Button _startFightButton;
        [SerializeField] private Image _icon;

        public Button ActiveAbilityButton => _activeAbilityButton;
        public Button StartFightButton => _startFightButton;

        public void InitAbility(IAbilityInfo abilityInfo)
        {
            if (abilityInfo != null)
            {
                _icon.sprite = abilityInfo.Icon;
            }
            else
            {
                _activeAbilityButton.gameObject.SetActive(false);
            }
        }
    }
}