using System;
using System.Collections.Generic;
using Game.Abilities;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Garage
{
    internal class GarageView : MonoBehaviour, IGarageView
    {
        [SerializeField] private GameObject _itemViewPrefab;
        [SerializeField] private Transform _placeForPassives;
        [SerializeField] private Transform _placeForActives;

        [SerializeField] private Button _backButton;

        private readonly Dictionary<string, ItemView> _itemViews = new Dictionary<string, ItemView>();


        public Button BackButton => _backButton;

        private void OnDestroy()
        {
            _backButton.onClick.RemoveAllListeners();
            Clear();
        }

        public void DisplayPassives(IEnumerable<IAbilityInfo> itemsCollection, Action<IAbilityInfo> itemClicked)
        {
            DisplayItems(itemsCollection, itemClicked, _placeForPassives);
        }

        public void DisplayActives(IEnumerable<IAbilityInfo> itemsCollection, Action<IAbilityInfo> itemClicked)
        {
            DisplayItems(itemsCollection, itemClicked, _placeForActives);
        }


        private void DisplayItems(IEnumerable<IAbilityInfo> itemsCollection, Action<IAbilityInfo> itemClicked,
            Transform place)
        {
            //Clear();

            foreach (var item in itemsCollection)
                _itemViews[item.ID] = CreateItemView(item, itemClicked, place);
        }

        public void Clear()
        {
            foreach (var itemView in _itemViews.Values)
                DestroyItemView(itemView);

            _itemViews.Clear();
        }


        public void Select(IAbilityInfo abilityInfo)
        {
            _itemViews[abilityInfo.ID].Select();
        }

        public void Unselect(IAbilityInfo abilityInfo)
        {
            _itemViews[abilityInfo.ID].Unselect();
        }


        private ItemView CreateItemView(IAbilityInfo item, Action<IAbilityInfo> itemClicked, Transform place)
        {
            var objectView = Instantiate(_itemViewPrefab, place, false);
            var itemView = objectView.GetComponent<ItemView>();

            itemView.Init
            (
                item,
                () => itemClicked?.Invoke(item)
            );

            return itemView;
        }

        private void DestroyItemView(ItemView itemView)
        {
            itemView.Deinit();
            Destroy(itemView.gameObject);
        }
    }
}