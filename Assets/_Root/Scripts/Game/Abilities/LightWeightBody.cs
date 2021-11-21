using Game.Transport;

namespace Game.Abilities
{
    internal sealed class LightWeightBody : PassiveAbility
    {
        public LightWeightBody(PassiveAbilityInfo abilityInfo) : base(abilityInfo)
        {
        }

        public override void Apply(TransportModel target, float valueMod = 1f) => target.Speed += _value;
    }
}
