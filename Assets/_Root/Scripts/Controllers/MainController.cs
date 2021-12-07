using Game.Fight;
using Game.Garage;
using Game.Models;
using Game.Utils;
using Services.Ads.UnityAds;
using Services.IAP;

namespace Game.Controllers
{
    internal class MainController : BaseController
    {
        private readonly IGameModel _gameModel;
        private readonly ResourceLoader _resourceLoader;
        private BaseController _currentController;

        public MainController(IGameModel gameModel, UnityAdsSettings unityAdsSettings, ProductLibrary productLibrary)
        {
            _gameModel = gameModel;
            _resourceLoader = new ResourceLoader();
            
            _gameModel.State.SubscribeOnChange(GameStateChanged);
            GameStateChanged(_gameModel.State.Value);
            
            UnityAdsService.Instance.Init(unityAdsSettings);
            IAPService.Instance.InitializeProducts(productLibrary);
        }

        private void GameStateChanged(GameState state)
        {
            _currentController?.Dispose();
            switch (state)
            {
                case GameState.MainMenu:
                    _currentController = new MainMenuController(_resourceLoader, _gameModel);
                    break;
                case GameState.RunGame:
                    _currentController = new GameController(_resourceLoader, _gameModel);
                    break;
                case GameState.SettingsMenu:
                    _currentController = new SettingsMenuController(_resourceLoader, _gameModel);
                    break;
                case GameState.Garage:
                    _currentController = new GarageController(_resourceLoader, _resourceLoader.LoadAbilitiesData(), _gameModel);
                    break;
                case GameState.Fight:
                    _currentController = new FightController(_gameModel, _resourceLoader);
                    break;
                default:
                    _currentController.Dispose();
                    break;
            }
        }


        protected override void OnDispose()
        {
            _currentController.Dispose();
            _gameModel.State.UnSubscribeOnChange(GameStateChanged);
        }
    }
}
