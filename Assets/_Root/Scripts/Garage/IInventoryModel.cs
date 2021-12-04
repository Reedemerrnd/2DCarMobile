
using System.Collections.Generic;
using Game.Abilities;

namespace Game.Garage
{
    internal interface IInventoryModel
    {
        public IReadOnlyList<IAbilityInfo> Passives { get; }
        public IAbilityInfo Active { get; }


        public void Equip(IAbilityInfo abilityInfo);
        public void UnEquip(IAbilityInfo abilityInfo);
        public bool IsEquipped(IAbilityInfo abilityInfo);
    }
}
