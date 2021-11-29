using System.Collections.Generic;

namespace Game.Garage
{
    internal class InventoryModel : IInventoryModel
    {
        private List<string> _passivesID;
        private string _activeID;

        public IReadOnlyList<string> Passives => _passivesID;
        public string Active => _activeID;
        
        public InventoryModel()
        {
            _passivesID = new List<string>();
        }


        public void Equip(string ID)
        {
            _passivesID.Add(ID);
        }

        public bool IsEquipped(string ID) => _passivesID.Contains(ID) || _activeID == ID;

        public void SetActive(string ID) => _activeID = ID;
        public void Unequip(string ID)
        {
            if (_activeID == ID)
            {
                _activeID = string.Empty;
                return;
            }
            _passivesID.Remove(ID);
        }
    }
}
