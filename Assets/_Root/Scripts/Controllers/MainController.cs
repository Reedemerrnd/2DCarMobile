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
        private MainMenuController _mainMenuController;
        private GameController _gameController;
        private SettingsMenuController _settingsController;
        private GarageController _garageController;

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
            DisposeAllControllers();
            switch (state)
            {
                case GameState.MainMenu:
                    _mainMenuController = new MainMenuController(_resourceLoader, _gameModel);
                    AddController(_mainMenuController);
                    break;
                case GameState.RunGame:
                    _gameController = new GameController(_resourceLoader, _gameModel);
                    AddController(_gameController);
                    break;
                case GameState.SettingsMenu:
                    _settingsController = new SettingsMenuController(_resourceLoader, _gameModel);
                    AddController(_settingsController);
                    break;
                case GameState.Garage:
                    _garageController = new GarageController(_resourceLoader, _resourceLoader.LoadAbilitiesData(), _gameModel);
                    AddController(_garageController);
                    break;
                default:
                    break;
            }
        }

        private void DisposeAllControllers()
        {
            _settingsController?.Dispose();
            _mainMenuController?.Dispose();
            _gameController?.Dispose();
            _garageController?.Dispose();
        }

        protected override void OnDispose()
        {
            _gameModel.State.UnSubscribeOnChange(GameStateChanged);
        }
    }
}
