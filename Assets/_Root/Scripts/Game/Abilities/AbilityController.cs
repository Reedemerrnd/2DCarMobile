using Game.Controllers;
using Game.Transport;

namespace Game.Abilities
{
    internal class AbilityController : BaseController
    {
        private readonly AbilityModel _abilityModel;
        private readonly TransportModel _transportModel;
        private readonly ITransportView _transportView;

        public AbilityController(ITransportView transportView, TransportModel transportModel, InGameUIView inGameUI,
            AbilityModel abilityModel)
        {
            _transportView = transportView;
            _transportModel = transportModel;
            _abilityModel = abilityModel;

            ApplyPassives();
            inGameUI.InitView(_abilityModel.Active, ButtonHandler);
        }

        private void ApplyPassives()
        {
            foreach (var passive in _abilityModel.Passives) passive.Apply(_transportView, _transportModel);
        }

        private void ButtonHandler()
        {
            _abilityModel.Active.Apply(_transportView, _transportModel);
        }
    }
}