using Game.Utils;
using Rewards;

namespace Game
{
    internal interface ICurrencyModel
    {
        IReadOnlySubscriptionProperty<int> Wood { get; }
        IReadOnlySubscriptionProperty<int> Diamond { get; }

        void SetCurrency(CurrencyType type, int value);

        void Save();
        void Reset();
    }
}