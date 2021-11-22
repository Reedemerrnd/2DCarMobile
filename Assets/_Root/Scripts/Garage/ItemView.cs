using Game.Abilities;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Garage
{
    internal class ItemView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Button _button;

        [SerializeField] private Toggle _selectedIndicator;

        private void OnDestroy() => Deinit();


        public void Init(IAbilityInfo item, UnityAction clickAction)
        {
            _icon.sprite = item.Icon;
            _button.onClick.AddListener(clickAction);
        }

        public void Deinit()
        {
            _icon.sprite = null;
            _button.onClick.RemoveAllListeners();
        }


        public void Select() => SetSelected(true);
        public void Unselect() => SetSelected(false);

        private void SetSelected(bool isSelected)
        {
            _selectedIndicator.isOn = isSelected;
        }
    }
}
