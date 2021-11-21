using Game.Transport;

namespace Game.Abilities
{
    internal abstract class ActiveAbility : Ability<ITransportView>
    {
        public ActiveAbilityType Type { get; protected set; }

        public ActiveAbility(ActiveAbilityInfo abilityInfo)
        {
            _value = abilityInfo.Value;
            Type = abilityInfo.Type;
        }
    }
}
