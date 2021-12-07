using Game.Transport;

namespace Game.Abilities
{
    internal class DefaultPassive : PassiveAbility
    {
        public DefaultPassive(PassiveAbilityInfo abilityInfo = null) : base(abilityInfo)
        {
        }

        public override void Apply(ITransportView view, ITransportModel model)
        {
        }
    }
}