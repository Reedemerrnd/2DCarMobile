using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game
{
    internal class SettingsMenu : MonoBehaviour
    {
        [SerializeField] private Button _backButton;

        public void Init(UnityAction backHandler)
        {
            _backButton.onClick.AddListener(backHandler);
        }

        private void OnDisable()
        {
            _backButton.onClick.RemoveAllListeners();
        }
    }
}
