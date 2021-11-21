using Game.Controllers;
using Game.Models;
using Game.Utils;
using Game.Views;
using UnityEngine;

namespace Game.Transport
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
        }
    }
}
