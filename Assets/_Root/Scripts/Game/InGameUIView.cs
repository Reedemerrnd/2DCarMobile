using Game.Abilities;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    internal class InGameUIView : MonoBehaviour, IInGameUIView
    {
        [SerializeField] private Button _activeAbilityButton;
        [SerializeField] private Image _icon;

        public Button ActiveAbilityButton => _activeAbilityButton;

        public void InitAbility(IAbilityInfo abilityInfo)
        {
            _activeAbilityButton.image.sprite = abilityInfo.Icon;
        }
    }
}