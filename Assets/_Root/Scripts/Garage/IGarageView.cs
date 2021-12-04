using Game.Abilities;
using System;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Garage
{
    internal interface IGarageView
    {
        public Button BackButton { get; }

        void DisplayPassives(IEnumerable<IAbilityInfo> itemsCollection, Action<IAbilityInfo> itemClicked);
        void DisplayActives(IEnumerable<IAbilityInfo> itemsCollection, Action<IAbilityInfo> itemClicked);
        void Select(IAbilityInfo abilityInfo);
        void Unselect(IAbilityInfo abilityInfo);
        void Clear();
    }
}