using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game
{
    internal class MainMenuView : MonoBehaviour
    {
        [SerializeField] private string _productIdToBuy;

        [SerializeField] private Button _startButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _rewarAdButton;
        [SerializeField] private Button _buyButton;

        public void Init(UnityAction startHandler, UnityAction settingsHandler, UnityAction playAdHandler, UnityAction<string> buyHandler)
        {
            _startButton.onClick.AddListener(startHandler);
            _settingsButton.onClick.AddListener(settingsHandler);
            _rewarAdButton.onClick.AddListener(playAdHandler);
            _buyButton.onClick.AddListener(() => buyHandler(_productIdToBuy));
        }

        private void OnDestroy()
        {
            _startButton.onClick.RemoveAllListeners();
            _settingsButton.onClick.RemoveAllListeners();
            _rewarAdButton.onClick.RemoveAllListeners();
            _buyButton.onClick.RemoveAllListeners();
        }
    }
}
