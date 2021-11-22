using Game.Transport;

namespace Game.Abilities
{
    internal abstract class ActiveAbility : Ability<ITransportView>
    {
        public ActiveAbilityType Type { get; protected set; }

        public ActiveAbility(ActiveAbilityInfo abilityInfo)
        {
            if(abilityInfo == null)
            {
                _value = 0;
                Type = ActiveAbilityType.None;
            }
        }
    }
}
