using System.Collections.Generic;

namespace Game.Abilities
{
    internal class AbilityModel
    {
        private readonly List<PassiveAbility> _passives;

        public AbilityModel(IEnumerable<PassiveAbility> passives, ActiveAbility active)
        {
            _passives = new List<PassiveAbility>(passives);
            Active = active;
        }

        public IReadOnlyCollection<PassiveAbility> Passives => _passives;

        public ActiveAbility Active { get; }
    }
}