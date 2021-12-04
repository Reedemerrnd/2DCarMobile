using System.Collections.Generic;

namespace Game.Garage
{
    internal class InventoryModel : IInventoryModel
    {
        private readonly List<string> _passivesID;
        private string _activeID;

        public IReadOnlyList<string> Passives => _passivesID;
        public string Active => _activeID;

        public InventoryModel() => _passivesID = new List<string>();


        public void Equip(string id) => _passivesID.Add(id);

        public bool IsEquipped(string id) => _passivesID.Contains(id) || _activeID == id;

        public void SetActive(string id) => _activeID = id;

        public void UnEquip(string id)
        {
            if (_activeID == id)
            {
                _activeID = string.Empty;
                return;
            }

            _passivesID.Remove(id);
        }
    }
}