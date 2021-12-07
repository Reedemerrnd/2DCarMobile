using Game.Controllers;
using Game.Models;
using Game.Utils;

namespace Game.Fight
{
    internal class FightController : BaseController
    {
        private readonly GameModel _gameModel;
        private readonly IInGameUIView _inGameUIView;
        private readonly IResourceLoader _resourceLoader;

        public FightController(GameModel gameModel, IInGameUIView inGameUIView, IResourceLoader resourceLoader)
        {
            _gameModel = gameModel;
            _inGameUIView = inGameUIView;
            _resourceLoader = resourceLoader;
        }

        private void ExitFightHandler() => _gameModel.UpdateState(GameState.RunGame);

        private void LoseFightHandler() => _gameModel.UpdateState(GameState.MainMenu);
    }
}