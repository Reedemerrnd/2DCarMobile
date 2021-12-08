using Game.Utils;

namespace Game
{
    internal interface ICurrencyModel
    {
        IReadOnlySubscriptionProperty<int> Wood { get; }
        IReadOnlySubscriptionProperty<int> Diamond { get; }
    }
}