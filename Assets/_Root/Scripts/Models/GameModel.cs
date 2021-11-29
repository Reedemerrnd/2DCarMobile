using Game.Utils;

namespace Game.Models
{
    internal class GameModel : IGameModel
    {
        private SubscriptionProperty<GameState> _state;
        private TransportType _transport;
        private float _speed;
        private InputType _input;

        public IReadOnlySubscriptionProperty<GameState> State => _state;
        public TransportType TransportType => _transport;
        public float Speed => _speed;
        public InputType InputType => _input;


        public GameModel(GameSettings gameSettings)
        {
            _state = new SubscriptionProperty<GameState>(GameState.MainMenu);
            _state.Value = gameSettings.State;
            _input = gameSettings.Input;
            _speed = gameSettings.Speed;
            _transport = gameSettings.TransportType;
        }


        public void UpdateState(GameState state)
        {
            _state.Value = state;
        }
    }
}
