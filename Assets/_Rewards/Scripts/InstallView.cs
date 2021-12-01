using UnityEngine;

namespace Rewards
{
    internal class InstallView : MonoBehaviour
    {
        [SerializeField] private TimeRewardView _timeRewardView;

        private TImeRewardController _timeRewardController;


        private void Awake() =>
            _timeRewardController = new TImeRewardController(_timeRewardView);

        private void Start() =>
            _timeRewardController.InitView();

        private void OnDestroy() =>
            _timeRewardController.Deinit();
    }
}