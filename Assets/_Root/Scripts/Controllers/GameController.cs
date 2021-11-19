using Game.Models;
using Game.Utils;
using Game.Views;
using UnityEngine;

namespace Game.Controllers
{
    internal class GameController : BaseController
    {
        private readonly IResourceLoader _resourceLoader;
        private readonly IGameModel _gameModel;
        private readonly CarView _carView;
        private readonly IInput _input;

        public GameController(IResourceLoader resourceLoader, IGameModel gameModel)
        {
            _resourceLoader = resourceLoader;
            _gameModel = gameModel;

            _input = LoadInput();
            _input.Init();

            var backgroundController = new BackgroundController(_resourceLoader, _input, new TransportModel(_gameModel.Speed));
            AddController(backgroundController);

            var carController = new TransportController(_resourceLoader, gameModel);
            AddController(carController);

        }

        private IInput LoadInput()
        {
            var inputPrefab = _resourceLoader.Load(InputType.Keyboard);
            var inputView = Object.Instantiate(inputPrefab);
            AddGameObject(inputPrefab.gameObject);
            return inputView.GetComponent<IInput>();
        }
    }
}
