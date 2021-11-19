using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game
{
    internal class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _rewarAdButton;

        public void Init(UnityAction startHandler, UnityAction settingsHandler, UnityAction playAdHandler)
        {
            _startButton.onClick.AddListener(startHandler);
            _settingsButton.onClick.AddListener(settingsHandler);
            _rewarAdButton.onClick.AddListener(playAdHandler);
        }

        private void OnDestroy()
        {
            _startButton.onClick.RemoveAllListeners();
            _settingsButton.onClick.RemoveAllListeners();
            _rewarAdButton.onClick.RemoveAllListeners();
        }
    }
}
