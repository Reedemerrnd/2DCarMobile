using Game.Abilities;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    internal class InGameUIView : MonoBehaviour, IInGameUIView
    {
        [field: SerializeField] public Button ActiveAbilityButton { get; private set; }
        [field: SerializeField] public Button StartFightButton { get; private set; }
        [field: SerializeField] public Button PauseButton { get; private set; }
        [field: SerializeField] public Button BackButton { get; private set; }
        
        [SerializeField] private Image _icon;
        

        public void InitAbility(IAbilityInfo abilityInfo)
        {
            if (abilityInfo != null)
            {
                _icon.sprite = abilityInfo.Icon;
            }
            else
            {
                ActiveAbilityButton.gameObject.SetActive(false);
            }
        }
    }
}