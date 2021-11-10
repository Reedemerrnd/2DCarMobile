using Game.Models;
using System;

namespace Game.Controllers
{
    internal class MainController : BaseController
    {
        private readonly IGameModel _gameModel;

        public MainController(IGameModel gameModel)
        {
            _gameModel = gameModel;
            _gameModel.State.SubscribeOnChange(GameStateChanged);
        }

        private void GameStateChanged(GameState state)
        {
            switch (state)
            {
                case GameState.MainMenu:
                    break;
                case GameState.RunGame:
                    var gameController = new GameController();
                    AddController(gameController);
                    break;

            }
        }
    }
}
