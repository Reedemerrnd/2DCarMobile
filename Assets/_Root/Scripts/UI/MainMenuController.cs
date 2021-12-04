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

        public MainMenuController(IUILoader uIloader, ISetGameState gameModel)
        {
            _gameModel = gameModel;
            var view = uIloader.Spawn<MainMenuView>(UIType.MainMenu, Vector3.zero, Quaternion.identity);
            AddGameObject(view.gameObject);
            view.Init(StarGame, OpenSettings, PlayRewardableAd, BuyProduct, OpenGarage);
        }

        private void StarGame() => _gameModel.UpdateState(GameState.RunGame);

        private void OpenSettings() => _gameModel.UpdateState(GameState.SettingsMenu);

        private void OpenGarage() => _gameModel.UpdateState(GameState.Garage);

        private void BuyProduct(string id)
        {
            SubscribeIAP();
            IAPService.Instance.Buy(id);
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
    }
}
