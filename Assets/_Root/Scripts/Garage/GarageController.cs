using Game.Abilities;
using Game.Controllers;
using Game.Models;
using Game.Utils;
using UnityEngine;

namespace Game.Garage
{
    internal class GarageController : BaseController
    {
        private readonly IUILoader _uILoader;
        private readonly IGameModel _gameModel;
        private readonly IInventoryModel _inventory;

        private readonly IGarageView _garageView;

        public GarageController(IUILoader uILoader, AbilitiesData abilitiesData, IGameModel gameModel)
        {
            _uILoader = uILoader;
            _gameModel = gameModel;
            _inventory = gameModel.Equipped;
            _garageView = LoadView();
            SubscribeButtons();


            _garageView.DisplayPassives(abilitiesData.Passives, OnItemCLicked);
            _garageView.DisplayActives(abilitiesData.Actives, OnItemCLicked);

            foreach (var item in _inventory.Passives)
            {
                _garageView.Select(item);
            }
            if(_inventory.Active != null)
            {
                _garageView.Select(_inventory.Active);
            }
        }

        private void SubscribeButtons()
        {
            _garageView.BackButton.onClick.AddListener(BackButtonHandler);
        }
        private void UnSubscribeButtons()
        {
            _garageView.BackButton.onClick.RemoveListener(BackButtonHandler);
        }
        
        
        private void BackButtonHandler() => _gameModel.UpdateState(GameState.MainMenu);

        private IGarageView LoadView()
        {
            var prefab = _uILoader.Load(UIType.Garage);
            var obj = Object.Instantiate(prefab);
            AddGameObject(obj);
            return obj.GetComponent<IGarageView>();
        }

        private void OnItemCLicked(IAbilityInfo abilityInfo)
        {
            if (_inventory.IsEquipped(abilityInfo))
            {
                _inventory.UnEquip(abilityInfo);
                _garageView.Unselect(abilityInfo);
            }
            else
            {
                _inventory.Equip(abilityInfo);
                _garageView.Select(abilityInfo);
            }
        }

        protected override void OnDispose()
        {
            UnSubscribeButtons();
            base.OnDispose();
        }
    }
}
