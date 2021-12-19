using Game.Transport;

namespace Game.Abilities
{
    internal sealed class Suspension : PassiveAbility
    {
        public Suspension(PassiveAbilityInfo abilityInfo) : base(abilityInfo)
        {
        }

        public override void Apply(ITransportView view, ITransportModel model)
        {
            model.JumpHeight += _value;
        }
    }
}