using Game.Controllers;
using Game.Transport;
using Game.Utils;
using Game.Views;

namespace Game
{
    internal class BackgroundController : BaseController
    {
        private readonly ILoadLevels _levelLoader;
        private readonly IInput _input;
        private readonly TransportModel _transportModel;
        private LevelBackgroundView _levelView;

        public BackgroundController(ILoadLevels levelLoader, IInput input, TransportModel transportModel)
        {
            _levelLoader = levelLoader;
            _input = input;
            _transportModel = transportModel;

            _input.XAxis.SubscribeOnChange(HandleInput);

            _levelView = _levelLoader.LoadLevel(1);
            AddGameObject(_levelView.gameObject);
        }


        protected override void OnDispose()
        {
            _input.XAxis.UnSubscribeOnChange(HandleInput);
            base.OnDispose();
        }

        private void HandleInput(float axis)
        {
            _levelView.Move(axis * _transportModel.Speed);
        }
    }
}
