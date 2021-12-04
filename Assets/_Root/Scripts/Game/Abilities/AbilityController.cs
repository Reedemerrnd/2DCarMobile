using Game.Controllers;
using Game.Transport;

namespace Game.Abilities
{
    internal class AbilityController : BaseController
    {
        private readonly AbilityModel _abilityModel;
        private readonly TransportModel _transportModel;
        private readonly InGameUIView _inGameUI;
        private readonly ITransportView _transportView;

        public AbilityController(ITransportView transportView, TransportModel transportModel, InGameUIView inGameUI,
            AbilityModel abilityModel)
        {
            _transportView = transportView;
            _transportModel = transportModel;
            _inGameUI = inGameUI;
            _abilityModel = abilityModel;

            ApplyPassives();
            _inGameUI.ActiveAbilityButton.onClick.AddListener(ActiveButtonHandler);
        }

        private void ApplyPassives()
        {
            foreach (var passive in _abilityModel.Passives) passive.Apply(_transportView, _transportModel);
        }

        private void ActiveButtonHandler()
        {
            _abilityModel.Active.Apply(_transportView, _transportModel);
        }

        protected override void OnDispose()
        {
            _inGameUI.ActiveAbilityButton.onClick.RemoveListener(ActiveButtonHandler);
            base.OnDispose();
        }
    }
}