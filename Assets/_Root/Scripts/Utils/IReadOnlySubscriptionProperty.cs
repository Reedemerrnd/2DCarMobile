using System;

namespace Game.Utils
{
    internal interface IReadOnlySubscriptionProperty<T>
    {
        public T Value { get; }

        public void SubscribeOnChange(Action<T> action);
        public void UnSubscribeOnChange(Action<T> action);
    }
}
