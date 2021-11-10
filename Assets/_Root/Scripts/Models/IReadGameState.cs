using Game.Utils;

namespace Game.Models
{
    internal interface IReadGameState
    {
        public IReadOnlySubscriptionProperty<GameState> State { get; }
    }
}
