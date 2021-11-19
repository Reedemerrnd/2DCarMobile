using Game.Views;

namespace Game.Utils
{
    internal interface ILoadLevels
    {
        public LevelBackgroundView LoadLevel(int index);
    }
}
