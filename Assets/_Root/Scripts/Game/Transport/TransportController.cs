using Game.Models;
using Game.Utils;
using Game.Views;
using UnityEngine;

namespace Game.Controllers
{
    internal class TransportController : BaseController
    {
        private readonly ITransportLoader _transportLoader;
        private readonly IGameModel _gameModel;
        private readonly TransportView _transportView;

        public TransportController(ITransportLoader transportLoader, IGameModel gameModel)
        {
            _transportLoader = transportLoader;
            _gameModel = gameModel;
            _transportView = _transportLoader.Spawn<TransportView>(_gameModel.TransportType, new Vector3(0f, 1.25f, 0f), Quaternion.identity);
            AddGameObject(_transportView.gameObject);
        }
    }
}
