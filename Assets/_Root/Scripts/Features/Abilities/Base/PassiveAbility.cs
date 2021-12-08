using Game.Transport;

namespace Game.Abilities
{
    internal abstract class PassiveAbility : Ability<ITransportView, ITransportModel>
    {
        public PassiveAbilityType Type { get; protected set; }

        protected PassiveAbility(PassiveAbilityInfo abilityInfo)
        {
            _value = abilityInfo.Value;
            Type = abilityInfo.Type;
        }
    }
}