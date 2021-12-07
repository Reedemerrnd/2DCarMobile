using System.Collections.Generic;
using Game.Abilities;

namespace Game.Garage
{
    internal class InventoryModel : IInventoryModel
    {
        private readonly List<IAbilityInfo> _passivesID;
        private IAbilityInfo _active;

        public IReadOnlyList<IAbilityInfo> Passives => _passivesID;
        public IAbilityInfo Active => _active;

        public InventoryModel() => _passivesID = new List<IAbilityInfo>();


        public void Equip(IAbilityInfo abilityInfo)
        {
            if (abilityInfo is ActiveAbilityInfo)
            {
                _active = abilityInfo;
                return;
            }

            _passivesID.Add(abilityInfo);
        }

        public bool IsEquipped(IAbilityInfo abilityInfo) => _passivesID.Contains(abilityInfo) || _active?.ID == abilityInfo.ID;

        public void UnEquip(IAbilityInfo abilityInfo)
        {
            if (_active.ID == abilityInfo.ID)
            {
                _active = null;
                return;
            }

            _passivesID.Remove(abilityInfo);
        }
    }
}