using Game.Controllers;
using Game.Transport;

namespace Game.Abilities
{
    internal class AbilityController : BaseController
    {
        private readonly ITransportView _transportView;
        private readonly TransportModel _transportModel;
        private readonly InGameUIView _inGameUI;
        private readonly AbilityModel _abilityModel;

        public AbilityController(ITransportView transportView, TransportModel transportModel, InGameUIView inGameUI, AbilityModel abilityModel)
        {
            _transportView = transportView;
            _transportModel = transportModel;
            _inGameUI = inGameUI;
            _abilityModel = abilityModel;

            _abilityModel.ApplyPassives(transportModel);
            _inGameUI.InitView(_abilityModel.Active, ButtonHandler);
        }

        private void ButtonHandler() => _abilityModel.Active.Apply(_transportView, _transportModel.JumpHeight);

    }
}
