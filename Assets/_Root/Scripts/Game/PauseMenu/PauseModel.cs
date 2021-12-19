using UnityEngine;

namespace Game
{
    public class PauseModel : IPauseModel
    {
        private float _currentTimeScale;

        public PauseModel(float initialTimeScale)
        {
            _currentTimeScale = initialTimeScale;
        }

        public void SetTimeScale(float timeScale) => _currentTimeScale = timeScale;

        public void Pause() => Time.timeScale = 0f;
        public void UnPause() => Time.timeScale = _currentTimeScale;
    }
}