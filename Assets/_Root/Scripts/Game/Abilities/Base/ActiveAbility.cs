using Game.Transport;

namespace Game.Abilities
{
    internal abstract class ActiveAbility : Ability<ITransportView, ITransportModel>

    {
        public ActiveAbilityType Type {  get; protected set; }

        protected ActiveAbility(ActiveAbilityInfo abilityInfo)
        {
            if (abilityInfo == null)
            {
                _value = 0;
                Type = ActiveAbilityType.None;
            }
            else
            {
                _value = abilityInfo.Value;
                Type = abilityInfo.Type;
            }
        }
    }
}
