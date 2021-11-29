using Game.Untils.ScreenBounds;
using UnityEngine;

namespace Game.Views
{
    public class BackgroundTile : MonoBehaviour
    {
        [SerializeField] private float _relativeSpeedRate;
        [SerializeField] private float _tileOffset;
        private float _leftBorder;
        private float _rightBorder;

        private void Awake()
        {
            _leftBorder = ScreenBounds.BottomLeft.x - _tileOffset;
            _rightBorder = ScreenBounds.TopRight.x + _tileOffset;
        }

        public void Move(float value)
        {
            Vector3 position = transform.position;
            position += Vector3.right * value * _relativeSpeedRate;

            if (position.x <= _leftBorder)
                position.x = _rightBorder - (_leftBorder - position.x);

            else if (position.x >= _rightBorder)
                position.x = _leftBorder + (_rightBorder - position.x);

            transform.position = position;
        }
    }
}
