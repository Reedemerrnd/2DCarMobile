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

        public GameController(IResourceLoader resourceLoader, IGameModel gameModel)
        {
            _resourceLoader = resourceLoader;
            _gameModel = gameModel;

            var carView = _resourceLoader.Spawn<TransportView>(_gameModel.TransportType, new Vector3(0f, 1.25f, 0f), Quaternion.identity);
            AddGameObject(carView.gameObject);

            var input = LoadInput();
            input.Init();

            var inGameUIView = _resourceLoader.Spawn<InGameUIView>(UIType.InGame, Vector3.zero, Quaternion.identity);
            inGameUIView.InitAbility(_gameModel.Equipped.Active);
            AddGameObject(inGameUIView.gameObject);
            
            var transportModel = new TransportModel(_gameModel.TransportType, _gameModel.Speed, _gameModel.JumpHeight);

            var backgroundController = new BackgroundController(_resourceLoader, input, transportModel);
            AddController(backgroundController);

            var carController = new TransportController(_resourceLoader, transportModel, carView);
            AddController(carController);

            var abilityModel = InitAbilityModel();
            var abiltyController = new AbilityController(carView, transportModel, inGameUIView, abilityModel);
            AddController(abiltyController);

            AnalyticsManager.Instance.SendEvent("Game Started");
            UnityAdsService.Instance.InterstitialPlayer.Play();
        }

        private AbilityModel InitAbilityModel()
        {
            var abilityData = _resourceLoader.LoadAbilitiesData();
            var factory = new AbilityFactory(_gameModel.Equipped, abilityData);
            return new AbilityModel(factory.CreatePassives(), factory.CreateActiveAbility());
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
