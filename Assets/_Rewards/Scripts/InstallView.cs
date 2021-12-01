using UnityEngine;

namespace Rewards
{
    internal class InstallView : MonoBehaviour
    {
        [SerializeField] private TimeRewardView _dailyRewardView;

        private TImeRewardController _dailyRewardController;


        private void Awake() =>
            _dailyRewardController = new TImeRewardController(_dailyRewardView);

        private void Start() =>
            _dailyRewardController.InitView();

        private void OnDestroy() =>
            _dailyRewardController.Deinit();
    }
}