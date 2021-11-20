using Game.Models;
using Game.Utils;
using Game.Views;
using UnityEngine;

namespace Game.Controllers
{
    internal class TransportController : BaseController
    {
        private readonly ITransportLoader _transportLoader;
        private readonly TransportModel _transportModel;
        private readonly TransportView _transportView;

        public TransportController(ITransportLoader transportLoader, TransportModel transportModel)
        {
            _transportLoader = transportLoader;
            _transportModel = transportModel;
            _transportView = _transportLoader.Spawn<TransportView>(_transportModel.Type, new Vector3(0f, 1.25f, 0f), Quaternion.identity);
            AddGameObject(_transportView.gameObject);
        }
    }
}
