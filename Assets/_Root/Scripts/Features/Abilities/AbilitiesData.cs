using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Abilities
{
    [CreateAssetMenu(menuName = "Abilities/Data")]
    internal class AbilitiesData : ScriptableObject
    {
        [field: SerializeField] public List<PassiveAbilityInfo> Passives { get; private set; }
        [field: SerializeField] public List<ActiveAbilityInfo> Actives { get; private set; }

        public PassiveAbilityInfo GetPassiveInfo(PassiveAbilityType type)
        {
            return Passives.FirstOrDefault(a => a.Type == type);
        }

        public PassiveAbilityInfo GetPassiveByID(string id)
        {
            return Passives.FirstOrDefault(a => a.ID == id);
        }

        public ActiveAbilityInfo GetActiveByID(string id)
        {
            return Actives.FirstOrDefault(a => a.ID == id);
        }

        public ActiveAbilityInfo GetActiveInfo(ActiveAbilityType type)
        {
            return Actives.FirstOrDefault(a => a.Type == type);
        }
    }
}