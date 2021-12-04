using System.Collections.Generic;
using System.Linq;
using Game.Garage;

namespace Game.Abilities
{
    internal class AbilityFactory
    {
        private readonly AbilitiesData _abilitiesData;
        private readonly IInventoryModel _equipped;

        public AbilityFactory(IInventoryModel equipped, AbilitiesData abilitiesData)
        {
            _equipped = equipped;
            _abilitiesData = abilitiesData;
        }


        public ActiveAbility CreateActiveAbility()
        {
            var ability = _abilitiesData.Actives.FirstOrDefault(a => a.ID == _equipped.Active);
            var type = ability == null ? ActiveAbilityType.None : ability.Type;
            return type switch
            {
                ActiveAbilityType.Jump => new JumpAbility(ability),
                ActiveAbilityType.None => new DefaultActive(),
                _ => new DefaultActive()
            };
        }

        public IEnumerable<PassiveAbility> GetPassives()
        {
            var passives = new List<PassiveAbility>(_equipped.Passives.Count);
            foreach (var ability in _equipped.Passives) passives.Add(CreatePassiveAbility(ability));
            return passives;
        }


        private PassiveAbility CreatePassiveAbility(string id)
        {
            var ability = _abilitiesData.Passives.FirstOrDefault(a => a.ID == id);
            var type = ability == null ? PassiveAbilityType.None : ability.Type;
            return type switch
            {
                PassiveAbilityType.LightWieghtBody => new LightWeightBody(ability),
                PassiveAbilityType.Suspension => new Suspension(ability),
                PassiveAbilityType.None => new DefaultPassive(),
                _ => new DefaultPassive()
            };
        }
    }
}