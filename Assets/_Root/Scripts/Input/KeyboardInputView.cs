using UnityEngine;
using Game.Utils;

namespace Game.Views
{
    internal class KeyboardInputView : MonoBehaviour, IInput
    {
        private SubscriptionProperty<float> _xAxis;

        public IReadOnlySubscriptionProperty<float> XAxis => _xAxis;


        public void Init()
        {
            _xAxis = new SubscriptionProperty<float>(0f);
        }


        void Update()
        {
            _xAxis.Value = Input.GetAxis("Horizontal");
        }
    }
}
