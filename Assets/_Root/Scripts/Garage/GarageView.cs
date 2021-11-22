using Game.Abilities;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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


        private void OnDestroy()
        {
            _backButton.onClick.RemoveAllListeners();
            Clear();
        }

        public void Init(UnityAction backButtonHandler)
        {
            _backButton.onClick.AddListener(backButtonHandler);
        }

        public void DisplayPassives(IEnumerable<IAbilityInfo> itemsCollection, Action<string> itemClicked)
        {
            DisplayItems(itemsCollection, itemClicked, _placeForPassives);
        }

        public void DisplayActives(IEnumerable<IAbilityInfo> itemsCollection, Action<string> itemClicked)
        {
            DisplayItems(itemsCollection, itemClicked, _placeForActives);
        }


        private void DisplayItems(IEnumerable<IAbilityInfo> itemsCollection, Action<string> itemClicked, Transform place)
        {
            //Clear();

            foreach (IAbilityInfo item in itemsCollection)
                _itemViews[item.ID] = CreateItemView(item, itemClicked, place);
        }

        public void Clear()
        {
            foreach (ItemView itemView in _itemViews.Values)
                DestroyItemView(itemView);

            _itemViews.Clear();
        }


        public void Select(string id) =>
            _itemViews[id].Select();

        public void Unselect(string id) =>
            _itemViews[id].Unselect();


        private ItemView CreateItemView(IAbilityInfo item, Action<string> itemClicked, Transform place)
        {
            GameObject objectView = Instantiate(_itemViewPrefab, place, false);
            ItemView itemView = objectView.GetComponent<ItemView>();

            itemView.Init
            (
                item,
                () => itemClicked?.Invoke(item.ID)
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
