using Game.Models;
using Game.Utils;
using Services.Ads.UnityAds;
using Services.IAP;
using UnityEngine;

namespace Game.Controllers
{
    internal class MainMenuController : BaseController
    {
        private readonly ISetGameState _gameModel;
        private readonly MainMenuView _view;
        private const string PRODUCT_ID_TO_BUY = "Consumale01";//for testing

        public MainMenuController(IUILoader uIloader, ISetGameState gameModel)
        {
            _gameModel = gameModel;
            _view = uIloader.Spawn<MainMenuView>(UIType.MainMenu, Vector3.zero, Quaternion.identity);
            AddGameObject(_view.gameObject);
            SubscribeButtons();
        }

        private void SubscribeButtons()
        {
            _view.BuyButton.onClick.AddListener(BuyProduct);
            _view.GarageButton.onClick.AddListener(OpenGarage);
            _view.SettingsButton.onClick.AddListener(OpenSettings);
            _view.StartButton.onClick.AddListener(StarGame);
            _view.RewardAdtButton.onClick.AddListener(PlayRewardableAd);
        }
        private void UnSubscribeButtons()
        {
            _view.BuyButton.onClick.RemoveListener(BuyProduct);
            _view.GarageButton.onClick.RemoveListener(OpenGarage);
            _view.SettingsButton.onClick.RemoveListener(OpenSettings);
            _view.StartButton.onClick.RemoveListener(StarGame);
            _view.RewardAdtButton.onClick.RemoveListener(PlayRewardableAd);
        }
        
        private void StarGame() => _gameModel.UpdateState(GameState.RunGame);

        private void OpenSettings() => _gameModel.UpdateState(GameState.SettingsMenu);

        private void OpenGarage() => _gameModel.UpdateState(GameState.Garage);

        private void BuyProduct()
        {
            SubscribeIAP();
            IAPService.Instance.Buy(PRODUCT_ID_TO_BUY);
        }

        private void SubscribeIAP()
        {
            IAPService.Instance.PurchaseFailed += PurchaseFailed;
            IAPService.Instance.PurchaseSucceed += PurchaseSucceded;
        }

        private void UnsubscribeIAP()
        {
            IAPService.Instance.PurchaseFailed -= PurchaseFailed;
            IAPService.Instance.PurchaseSucceed -= PurchaseSucceded;
        }

        private void PurchaseSucceded()
        {
            UnsubscribeIAP();
            Debug.Log("Purchased");
        }

        private void PurchaseFailed()
        {
            UnsubscribeIAP();
            Debug.Log("Purchase failed");
        }

        private void PlayRewardableAd()
        {
            SubscribeRewardedAd();
            UnityAdsService.Instance.RewardedPlayer.Play();
        }

        private void SubscribeRewardedAd()
        {
            UnityAdsService.Instance.RewardedPlayer.Finished += RewardedSucces;
            UnityAdsService.Instance.RewardedPlayer.Failed += RewardedFailed;
            UnityAdsService.Instance.RewardedPlayer.Skipped += RewardedFailed;
        }

        private void UnsubscribeRewardedAd()
        {
            UnityAdsService.Instance.RewardedPlayer.Finished -= RewardedSucces;
            UnityAdsService.Instance.RewardedPlayer.Failed -= RewardedFailed;
            UnityAdsService.Instance.RewardedPlayer.Skipped -= RewardedFailed;
        }

        private void RewardedFailed()
        {
            Debug.Log("Rewarded failed");
            UnsubscribeRewardedAd();
        }

        private void RewardedSucces()
        {
            Debug.Log("Rewarded succes");
            UnsubscribeRewardedAd();
        }

        protected override void OnDispose()
        {
            UnSubscribeButtons();
            base.OnDispose();
        }
    }
}
