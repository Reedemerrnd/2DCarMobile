using Game.Transport;

namespace Game.Abilities
{
    internal class DefaultActive : ActiveAbility
    {
        public DefaultActive(ActiveAbilityInfo abilityInfo = null) : base(abilityInfo)
        {
        }

        public override void Apply(ITransportView target, float valueMod = 1f)
        {
        }
    }
}
