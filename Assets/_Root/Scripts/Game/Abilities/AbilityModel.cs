using Game.Transport;
using System;
using System.Collections.Generic;

namespace Game.Abilities
{
    internal class AbilityModel
    {
        private List<PassiveAbility> _passives;
        public ActiveAbility Active { get; private set; }

        public AbilityModel(IEnumerable<PassiveAbility> passives, ActiveAbility active)
        {
            _passives = new List<PassiveAbility>(passives);
            Active = active;
        }


        public void ApplyPassives(TransportModel transportModel)
        {
            foreach (var passive in _passives)
            {
                passive.Apply(transportModel);
            }
        }


    }
}
