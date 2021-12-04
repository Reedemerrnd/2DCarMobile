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
        private readonly AbilitiesData _abilitiesData;
        private readonly IGameModel _gameModel;
        private readonly IInventoryModel _inventory;

        private IGarageView _garageView;

        public GarageController(IUILoader uILoader, AbilitiesData abilitiesData, IGameModel gameModel)
        {
            _uILoader = uILoader;
            _abilitiesData = abilitiesData;
            _gameModel = gameModel;
            _inventory = gameModel.Equipped;
            _garageView = LoadView();
            _garageView.Init(BackButtonHandler);


            _garageView.DisplayPassives(_abilitiesData.Passives, OnPassiveCLicked);
            _garageView.DisplayActives(_abilitiesData.Actives, OnActiveClicked);

            foreach (var item in _inventory.Passives)
            {
                _garageView.Select(item);
            }
            if(_inventory.Active != null)
            {
                _garageView.Select(_inventory.Active);
            }
        }

        private void BackButtonHandler() => _gameModel.UpdateState(GameState.MainMenu);

        private IGarageView LoadView()
        {
            var prefab = _uILoader.Load(UIType.Garage);
            var obj = Object.Instantiate(prefab);
            AddGameObject(obj);
            return obj.GetComponent<IGarageView>();
        }

        private void OnPassiveCLicked(string id)
        {
            if (_inventory.IsEquipped(id))
            {
                _inventory.UnEquip(id);
                _garageView.Unselect(id);
            }
            else
            {
                _inventory.Equip(id);
                _garageView.Select(id);
            }
        }

        private void OnActiveClicked(string id)
        {
            if (_inventory.IsEquipped(id))
            {
                _inventory.UnEquip(id);
                _garageView.Unselect(id);
            }
            else
            {
                _inventory.SetActive(id);
                _garageView.Select(id);
            }
        }
    }
}
