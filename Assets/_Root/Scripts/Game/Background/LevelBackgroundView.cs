using UnityEngine;

namespace Game.Views
{
    internal class LevelBackgroundView : MonoBehaviour
    {
        [SerializeField] private BackgroundTile[] _backgrounds;


        public void Move(float value)
        {
            foreach (var background in _backgrounds)
                background.Move(-value);
        }
    }
}
