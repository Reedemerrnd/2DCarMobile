using Game.Abilities;
using Game.Models;
using Game.Transport;
using Game.Utils;
using Game.Views;
using Services.Ads.UnityAds;
using Services.Analytics;
using UnityEngine;

namespace Game.Controllers
{
    internal class GameController : BaseController
    {
        private readonly IResourceLoader _resourceLoader;
        private readonly IGameModel _gameModel;
        private readonly TransportView _carView;
        private readonly IInput _input;
        private readonly InGameUIView _uIView;
        private AbilityModel _abilityModel;

        public GameController(IResourceLoader resourceLoader, IGameModel gameModel)
        {
            _resourceLoader = resourceLoader;
            _gameModel = gameModel;

            _carView = _resourceLoader.Spawn<TransportView>(_gameModel.TransportType, new Vector3(0f, 1.25f, 0f), Quaternion.identity);
            AddGameObject(_carView.gameObject);

            _input = LoadInput();
            _input.Init();

            _uIView = _resourceLoader.Spawn<InGameUIView>(UIType.InGame, Vector3.zero, Quaternion.identity);
            AddGameObject(_uIView.gameObject);

            var transportModel = new TransportModel(_gameModel.TransportType, _gameModel.Speed, _gameModel.JumpHeight);

            var backgroundController = new BackgroundController(_resourceLoader, _input, transportModel);
            AddController(backgroundController);

            var carController = new TransportController(_resourceLoader, transportModel);
            AddController(carController);

            InitAbilities();
            //temp
            var abiltyController = new AbilityController(_carView, transportModel, _uIView, _abilityModel);
            AddController(abiltyController);

            AnalyticsManager.Instance.SendEvent("Game Started");
            UnityAdsService.Instance.InterstitialPlayer.Play();
        }

        //temp
        private void InitAbilities()
        {
            _abilityModel = new AbilityModel();
            var abilityData = _resourceLoader.LoadAbilitiesData();
            var factory = new AbilityFactory(abilityData);

            _abilityModel.AddPassive(factory.GetPassiveAbility(PassiveAbilityType.LightWieghtBody));
            _abilityModel.AddPassive(factory.GetPassiveAbility(PassiveAbilityType.Suspension));

            _abilityModel.SetActive(factory.GetActiveAbility(ActiveAbilityType.Jump));
        }

        private IInput LoadInput()
        {
            var inputPrefab = _resourceLoader.Load(InputType.Keyboard);
            var inputView = Object.Instantiate(inputPrefab);
            AddGameObject(inputPrefab.gameObject);
            return inputView.GetComponent<IInput>();
        }
    }
}
