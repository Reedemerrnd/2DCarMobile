using Game.Transport;

namespace Game.Abilities
{
    internal sealed class LightWeightBody : PassiveAbility
    {
        public LightWeightBody(PassiveAbilityInfo abilityInfo) : base(abilityInfo)
        {
        }

        public override void Apply(ITransportView view, ITransportModel model)
        {
            model.Speed += _value;
        }
    }
}