using Game.Transport;

namespace Game.Abilities
{
    internal abstract class PassiveAbility : Ability<TransportModel>
    {
        public PassiveAbilityType Type { get; protected set; }

        public PassiveAbility(PassiveAbilityInfo abilityInfo)
        {
            _value = abilityInfo.Value;
            Type = abilityInfo.Type;
            Icon = abilityInfo.Icon;
        }
    }
}
