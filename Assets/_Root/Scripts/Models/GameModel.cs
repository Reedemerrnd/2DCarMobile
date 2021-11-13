using Game.Utils;
using UnityEngine;

namespace Game.Models
{
    [CreateAssetMenu(menuName = "Game/GameModel")]
    internal class GameModel : ScriptableObject, IGameModel
    {
        private SubscriptionProperty<GameState> _state;

        [Header("Transport settings")]
        [SerializeField] private TransportType _transport;
        [SerializeField] private float _speed;

        [Header("InputSettings")]
        [SerializeField] private InputType _input;

        public IReadOnlySubscriptionProperty<GameState> State => _state;
        public TransportType TransportType => _transport;
        public float Speed => _speed;
        public InputType InputType => _input;

        public void Init()
        {
            _state = new SubscriptionProperty<GameState>(GameState.MainMenu);
        }


        public void UpdateState(GameState state)
        {
            _state.Value = state;
        }
    }
}
