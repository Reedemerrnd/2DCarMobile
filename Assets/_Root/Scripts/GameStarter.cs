using Game.Controllers;
using Game.Models;
using UnityEngine;

namespace Game
{
    internal class GameStarter : MonoBehaviour
    {
        [SerializeField] private TransportType _transport;
        private const GameState INITIAL_STATE = GameState.MainMenu;
        private MainController _mainController;

        private void Awake()
        {
            var gameModel = new GameModel(INITIAL_STATE);
            _mainController = new MainController(gameModel);
        }


    }
}
