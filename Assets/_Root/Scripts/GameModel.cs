using Game.Abilities;
using Game.Garage;
using Game.Utils;

namespace Game.Models
{
    internal class GameModel : IGameModel
    {
        private SubscriptionProperty<GameState> _state;
        private TransportType _transport;
        private float _speed;
        private InputType _input;
        private float _jumpHeight;
        private InventoryModel _inventoryModel;
        private ICurrencyModel _currencyModel;
        private readonly PauseModel _pauseModel;

        public IReadOnlySubscriptionProperty<GameState> State => _state;
        public TransportType TransportType => _transport;
        public float Speed => _speed;
        public float JumpHeight => _jumpHeight;
        public InputType InputType => _input;
        public IInventoryModel Equipped => _inventoryModel;
        public ICurrencyModel Currencies => _currencyModel;
        public IPauseModel Pause => _pauseModel;


        public GameModel(GameSettings gameSettings)
        {
            _state = new SubscriptionProperty<GameState>(GameState.MainMenu);
            _state.Value = gameSettings.State;
            _input = gameSettings.Input;
            _speed = gameSettings.Speed;
            _jumpHeight = gameSettings.JumpHeight;
            _transport = gameSettings.TransportType;

            _pauseModel = new PauseModel(1f);
            _inventoryModel = new InventoryModel();
            _currencyModel = new PlayerPrefsCurrencyModel();
        }


        public void UpdateState(GameState state)
        {
            _state.Value = state;
        }
    }
}
