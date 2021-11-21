using Game.Abilities;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game
{
    internal class InGameUIView : MonoBehaviour
    {
        [SerializeField] private Button _activeAbilityButton;

        public void InitView(ActiveAbility ability, UnityAction buttonHandler)
        {
            _activeAbilityButton.onClick.AddListener(buttonHandler);
            _activeAbilityButton.image.sprite = ability.Icon;
        }

        private void OnDisable()
        {
            _activeAbilityButton.onClick.RemoveAllListeners();
        }
    }
}
