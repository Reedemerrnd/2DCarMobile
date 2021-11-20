using Game.Models;
using Game.Utils;
using Services.Ads.UnityAds;
using UnityEngine;

namespace Game.Controllers
{
    internal class MainMenuController : BaseController
    {
        private readonly IUILoader _uIloader;
        private readonly ISetGameState _gameModel;
        private readonly MainMenuView _view;

        public MainMenuController(IUILoader UIloader, ISetGameState gameModel)
        {
            _uIloader = UIloader;
            _gameModel = gameModel;
            _view = _uIloader.Spawn<MainMenuView>(UIType.MainMenu, Vector3.zero, Quaternion.identity);
            AddGameObject(_view.gameObject);
            _view.Init(StarGame, OpenSettings, PlayRewardableAd);
        }

        private void StarGame() => _gameModel.UpdateState(GameState.RunGame);

        private void OpenSettings() => _gameModel.UpdateState(GameState.SettingsMenu);

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
        }

        private void RewardedSucces()
        {
            Debug.Log("Rewarded succes");

        }
    }
}
