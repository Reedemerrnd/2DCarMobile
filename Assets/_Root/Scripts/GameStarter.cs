using Game.Controllers;
using Game.Models;
using UnityEngine;

namespace Game
{
    internal class GameStarter : MonoBehaviour
    {
        [SerializeField] private GameModel _gameModel;
        private MainController _mainController;

        private void Start()
        {
            _gameModel.Init();
            _mainController = new MainController(_gameModel);
        }

        private void OnDestroy()
        {
            _mainController.Dispose();
        }
    }
}
