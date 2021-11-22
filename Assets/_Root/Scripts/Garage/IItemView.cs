
using Game.Abilities;
using System;

namespace Game.Garage
{
    internal interface IItemView
    {
        public void Init(IAbilityInfo ability, Action clicked);

        public void Deinit();
    }
}
