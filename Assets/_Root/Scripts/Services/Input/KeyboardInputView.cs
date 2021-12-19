using Game.Utils;
using UnityEngine;

namespace Game.Views
{
    internal class KeyboardInputView : MonoBehaviour, IInput
    {
        private SubscriptionProperty<float> _xAxis;
        private bool _isLocked;
        public IReadOnlySubscriptionProperty<float> XAxis => _xAxis;


        public void Init()
        {
            _xAxis = new SubscriptionProperty<float>(0f);
        }

        public void Lock()
        {
            _isLocked = true;
        }

        public void Unlock()
        {
            _isLocked = false;
        }


        private void Update()
        {
            if (!_isLocked) _xAxis.Value = Input.GetAxis("Horizontal");
        }
    }
}