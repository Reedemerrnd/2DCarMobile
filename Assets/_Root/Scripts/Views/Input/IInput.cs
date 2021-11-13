using Game.Utils;

namespace Game.Views
{
    internal interface IInput
    {
        public IReadOnlySubscriptionProperty<float> XAxis { get; }
        public void Init();

    }
}
