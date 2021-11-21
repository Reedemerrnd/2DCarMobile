using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Abilities
{
    internal class AbilityFactory
    {
        private readonly AbilitiesData _abilitiesData;

        public AbilityFactory(AbilitiesData abilitiesData)
        {
            _abilitiesData = abilitiesData;
        }


        public ActiveAbility GetActiveAbility(ActiveAbilityType type)
        {
            return type switch
            {
                ActiveAbilityType.Jump => new JumpAbility(_abilitiesData.GetActiveInfo(type)),
                ActiveAbilityType.None => new DefaultActive(),
                _ => new DefaultActive()
            };
        }

        public PassiveAbility GetPassiveAbility(PassiveAbilityType type)
        {
            return type switch
            {
                PassiveAbilityType.LightWieghtBody => new LightWeightBody(_abilitiesData.GetPassiveInfo(type)),
                PassiveAbilityType.Suspension => new Suspension(_abilitiesData.GetPassiveInfo(type)),
                PassiveAbilityType.None => new DefaultPassive(),
                _  => new DefaultPassive()
            };
        }
    }
}
