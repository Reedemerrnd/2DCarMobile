
using System.Collections.Generic;

namespace Game.Garage
{
    internal interface IInventoryModel
    {
        public IReadOnlyList<string> Passives { get; }
        public string Active { get; }


        public void Equip(string ID);
        public void Unequip(string ID);
        public bool IsEquipped(string ID);

        public void SetActive(string ID);
    }
}
