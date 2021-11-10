using Game.Utils;

namespace Game.Models
{
    internal class GameModel : IGameModel
    {
        private SubscriptionProperty<GameState> _state;


        public IReadOnlySubscriptionProperty<GameState> State => _state;


        public GameModel(GameState state)
        {
            _state = new SubscriptionProperty<GameState>(state);
        }


        public void UpdateState(GameState state)
        {
            _state.Value = state;
        }
    }
}
