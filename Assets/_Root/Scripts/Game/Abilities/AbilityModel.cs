using Game.Transport;
using System;
using System.Collections.Generic;

namespace Game.Abilities
{
    [Serializable]
    internal class AbilityModel
    {
        private List<PassiveAbility> _passives;
        public ActiveAbility Active { get; private set; }

        public AbilityModel()
        {
            _passives = new List<PassiveAbility>();
        }

        public void AddPassive(PassiveAbility passiveAbility) => _passives.Add(passiveAbility);

        public void SetActive(ActiveAbility activeAbility) => Active = activeAbility;

        public void ApplyPassives(TransportModel transportModel)
        {
            foreach (var passive in _passives)
            {
                passive.Apply(transportModel);
            }
        }


    }
}
