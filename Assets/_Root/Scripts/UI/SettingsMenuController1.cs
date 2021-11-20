using Game.Models;
using Game.Utils;
using UnityEngine;

namespace Game.Controllers
{
    internal class SettingsMenuController : BaseController
    {
        private readonly IUILoader _uIloader;
        private readonly ISetGameState _gameModel;
        private readonly SettingsMenu _view;

        public SettingsMenuController(IUILoader UIloader, ISetGameState gameModel)
        {
            _uIloader = UIloader;
            _gameModel = gameModel;
            _view = _uIloader.Spawn<SettingsMenu>(UIType.SettingsMenu, Vector3.zero, Quaternion.identity);
            AddGameObject(_view.gameObject);
            _view.Init(CloseSettings);
        }

        private void CloseSettings() => _gameModel.UpdateState(GameState.MainMenu);

    }
}
