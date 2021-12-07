using Game.Models;
using Game.Utils;
using UnityEngine;

namespace Game.Controllers
{
    internal class SettingsMenuController : BaseController
    {
        private readonly ISetGameState _gameModel;
        private readonly SettingsMenuView _view;

        public SettingsMenuController(IUILoader UIloader, ISetGameState gameModel)
        {
            _gameModel = gameModel;
            _view = UIloader.Spawn<SettingsMenuView>(UIType.SettingsMenu, Vector3.zero, Quaternion.identity);
            AddGameObject(_view.gameObject);
            _view.BackButton.onClick.AddListener(CloseSettings);
        }

        private void CloseSettings() => _gameModel.UpdateState(GameState.MainMenu);

        protected override void OnDispose()
        {
            _view.BackButton.onClick.RemoveListener(CloseSettings);
            base.OnDispose();
        }
    }
}
