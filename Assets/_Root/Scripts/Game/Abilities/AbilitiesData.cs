using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Abilities
{
    [CreateAssetMenu (menuName = "Abilities/Data")]
    internal class AbilitiesData : ScriptableObject
    {
        [field: SerializeField] public List<PassiveAbilityInfo> Passives { get; private set; }
        [field: SerializeField] public List<ActiveAbilityInfo> Actives { get; private set; }

        public PassiveAbilityInfo GetPassiveInfo(PassiveAbilityType type) => Passives.FirstOrDefault((a) => a.Type == type);
        public ActiveAbilityInfo GetActiveInfo(ActiveAbilityType type) => Actives.FirstOrDefault((a) => a.Type == type);
    }
}
