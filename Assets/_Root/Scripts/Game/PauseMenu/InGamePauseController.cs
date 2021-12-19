using Game.Controllers;
using Game.Models;
using Game.Utils;
using UnityEngine;

namespace Game
{
    internal class InGamePauseController : BaseController
    {
        private readonly InGameUIView _inGameUIView;
        private readonly PauseMenuView _pauseMenuView;

        private readonly IResourceLoader _resourceLoader;
        private readonly IGameModel _gameModel;

        public InGamePauseController(InGameUIView inGameUIView, IResourceLoader resourceLoader, IGameModel gameModel)
        {
            _inGameUIView = inGameUIView;
            _resourceLoader = resourceLoader;
            _gameModel = gameModel;

            _pauseMenuView = LoadPauseView();
            _pauseMenuView.Hide();
            SubscribeButtons();
        }

        private PauseMenuView LoadPauseView()
        {
            var view = _resourceLoader.Spawn<PauseMenuView>(UIType.PauseMenu, Vector3.zero, Quaternion.identity);
            AddGameObject(view.gameObject);
            return view;
        }

        private void SubscribeButtons()
        {
            _inGameUIView.PauseButton.onClick.AddListener(PauseGame);
            _pauseMenuView.ContinueButton.onClick.AddListener(UnPauseGame);
            _pauseMenuView.MainMenuButton.onClick.AddListener(OpenMainMenu);
        }

        private void UnSubscribeButtons()
        {
            _inGameUIView.PauseButton.onClick.RemoveListener(PauseGame);
            _pauseMenuView.ContinueButton.onClick.RemoveListener(UnPauseGame);
            _pauseMenuView.MainMenuButton.onClick.RemoveListener(OpenMainMenu);
        }

        private void OpenMainMenu()
        {
            _gameModel.UpdateState(GameState.MainMenu);
        }

        private void PauseGame()
        {
            _gameModel.Pause.Pause();
            _pauseMenuView.Show();
        }

        private void UnPauseGame()
        {
            _gameModel.Pause.UnPause();
            _pauseMenuView.Hide();
        }

        protected override void OnDispose()
        {
            UnSubscribeButtons();
            base.OnDispose();
        }
    }
}