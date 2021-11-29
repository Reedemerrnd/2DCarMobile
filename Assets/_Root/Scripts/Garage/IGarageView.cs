
using Game.Abilities;
using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Game.Garage
{
    internal interface IGarageView
    {
        public void DisplayPassives(IEnumerable<IAbilityInfo> itemsCollection, Action<string> itemClicked);
        public void DisplayActives(IEnumerable<IAbilityInfo> itemsCollection, Action<string> itemClicked);
        void Select(string id);
        void Unselect(string id);
        void Clear();
        void Init(UnityAction backButtonHandler);
    }
}
