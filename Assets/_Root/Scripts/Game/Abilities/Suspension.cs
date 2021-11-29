using Game.Transport;

namespace Game.Abilities
{
    internal sealed class Suspension : PassiveAbility
    {
        public Suspension(PassiveAbilityInfo abilityInfo) : base(abilityInfo)
        {
        }

        public override void Apply(TransportModel target, float valueMod = 1f) => target.JumpHeight += _value;
    }
}
