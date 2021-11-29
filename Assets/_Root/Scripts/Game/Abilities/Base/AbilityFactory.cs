using Game.Garage;
using System.Collections.Generic;
using System.Linq;

namespace Game.Abilities
{
    internal class AbilityFactory
    {
        private readonly IInventoryModel _equipped;
        private readonly AbilitiesData _abilitiesData;

        public AbilityFactory(IInventoryModel equipped, AbilitiesData abilitiesData)
        {
            _equipped = equipped;
            _abilitiesData = abilitiesData;
        }


        public ActiveAbility GetActiveAbility()
        {
            var ability = _abilitiesData.Actives.FirstOrDefault(a => a.ID == _equipped.Active);
            var type = ability == null ? ActiveAbilityType.None : ability.Type;
            return type switch
            {
                ActiveAbilityType.Jump => new JumpAbility(_abilitiesData.GetActiveInfo(type)),
                ActiveAbilityType.None => new DefaultActive(),
                _ => new DefaultActive()
            };
        }

        public IEnumerable<PassiveAbility> GetPassives()
        {
            List<PassiveAbility> passives = new List<PassiveAbility>(_equipped.Passives.Count);
            foreach (var ability in _equipped.Passives)
            {
                passives.Add(GetPassiveAbility(ability));
            }
            return passives;
        }


        private PassiveAbility GetPassiveAbility(string id)
        {
            var type = _abilitiesData.Passives.FirstOrDefault(a => a.ID == id).Type;
            return type switch
            {
                PassiveAbilityType.LightWieghtBody => new LightWeightBody(_abilitiesData.GetPassiveInfo(type)),
                PassiveAbilityType.Suspension => new Suspension(_abilitiesData.GetPassiveInfo(type)),
                PassiveAbilityType.None => new DefaultPassive(),
                _ => new DefaultPassive()
            };
        }
    }
}
