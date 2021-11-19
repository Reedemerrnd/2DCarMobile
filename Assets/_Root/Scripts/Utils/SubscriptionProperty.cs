using System;

namespace Game.Utils
{
    internal class SubscriptionProperty<T> : IReadOnlySubscriptionProperty<T>
    {
        private T _value;
        private Action<T> _valueChanged;

        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                _valueChanged?.Invoke(_value);
            }
        }


        public SubscriptionProperty(T value)
        {
            Value = value;
        }


        public void SubscribeOnChange(Action<T> action) => _valueChanged += action;
        public void UnSubscribeOnChange(Action<T> action) => _valueChanged -= action;
    }
}
