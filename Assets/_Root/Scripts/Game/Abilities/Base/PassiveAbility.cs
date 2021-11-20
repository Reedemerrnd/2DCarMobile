using Game.Models;

namespace Game.Abilities
{
    internal abstract class PassiveAbility : Ability<TransportModel>
    {
        public PassiveAbilityType Type { get; protected set; }

        public PassiveAbility(float value) : base(value)
        {

        }
    }
}
