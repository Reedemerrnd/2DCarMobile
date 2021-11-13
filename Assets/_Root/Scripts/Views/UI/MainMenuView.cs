using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game
{
    internal class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _settingsButton;

        public void Init(UnityAction startHandler, UnityAction settingsHandler)
        {
            _startButton.onClick.AddListener(startHandler);
            _settingsButton.onClick.AddListener(settingsHandler);
        }

        private void OnDestroy()
        {
            _startButton.onClick.RemoveAllListeners();
            _settingsButton.onClick.RemoveAllListeners();
        }
    }
}
