using Game.Controllers;
using Game.Models;
using Services.Ads.UnityAds;
using UnityEngine;

namespace Game
{
    internal class GameStarter : MonoBehaviour
    {
        [SerializeField] private GameSettings _gameSettings;
        [SerializeField] private UnityAdsSettings _adsSettings;
        private GameModel _gameModel;
        private MainController _mainController;

        private void Start()
        {
            _gameModel = new GameModel(_gameSettings);
            _mainController = new MainController(_gameModel, _adsSettings);
        }

        private void OnDestroy()
        {
            _mainController.Dispose();
        }
    }
}
