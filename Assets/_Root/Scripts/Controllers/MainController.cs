using Game.Models;
using Game.Utils;

namespace Game.Controllers
{
    internal class MainController : BaseController
    {
        private readonly IGameModel _gameModel;
        private readonly ResourceLoader _resourceLoader;
        private MainMenuController _mainMenuController;
        private GameController _gameController;
        private SettingsMenuController _settingsController;

        public MainController(IGameModel gameModel)
        {
            _gameModel = gameModel;
            _resourceLoader = new ResourceLoader();
            _gameModel.State.SubscribeOnChange(GameStateChanged);
            GameStateChanged(_gameModel.State.Value);
        }

        private void GameStateChanged(GameState state)
        {
            switch (state)
            {
                case GameState.MainMenu:
                    _mainMenuController = new MainMenuController(_resourceLoader, _gameModel);
                    AddController(_mainMenuController);
                    _settingsController?.Dispose();
                    _gameController?.Dispose();
                    break;
                case GameState.RunGame:
                    _gameController = new GameController(_resourceLoader, _gameModel);
                    AddController(_gameController);
                    _settingsController?.Dispose();
                    _mainMenuController?.Dispose();
                    break;
                case GameState.SettingsMenu:
                    _settingsController = new SettingsMenuController(_resourceLoader, _gameModel);
                    AddController(_settingsController);
                    _mainMenuController?.Dispose();
                    _gameController?.Dispose();
                    break;
                default:
                    _settingsController?.Dispose();
                    _mainMenuController?.Dispose();
                    _gameController?.Dispose();
                    break;
            }
        }

        protected override void OnDispose()
        {
            _gameModel.State.UnSubscribeOnChange(GameStateChanged);
        }
    }
}
