namespace Game
{
    public interface IPauseModel
    {
        void SetTimeScale(float timeScale);
        void Pause();
        void UnPause();
    }
}