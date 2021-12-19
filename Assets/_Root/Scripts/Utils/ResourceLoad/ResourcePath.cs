using System.Collections.Generic;

namespace Game.Utils.ResourcesLoad
{
    internal static class ResourcePath
    {
        private const string _googleDiskUrl = "https://drive.google.com/uc?export=download&id=";
        
        public static Dictionary<TransportType, string> Transport = new Dictionary<TransportType, string>()
        {
            {TransportType.Car, $"Prefabs/Transport/{TransportType.Car}" },
            {TransportType.Boat, $"Prefabs/Transport/{TransportType.Boat}" }
        };

        public static Dictionary<UIType, string> UI = new Dictionary<UIType, string>()
        {
            {UIType.MainMenu, $"Prefabs/UI/{UIType.MainMenu}" },
            {UIType.SettingsMenu, $"Prefabs/UI/{UIType.SettingsMenu}" },
            {UIType.InGame, $"Prefabs/UI/{UIType.InGame}" },
            {UIType.Garage, $"Prefabs/UI/{UIType.Garage}" },
            {UIType.Fight, $"Prefabs/UI/{UIType.Fight}" },
            {UIType.Rewards, $"Prefabs/UI/{UIType.Rewards}" },
            {UIType.Currency, $"Prefabs/UI/{UIType.Currency}" },
            {UIType.PauseMenu, $"Prefabs/UI/{UIType.PauseMenu}" },
        };

        public static Dictionary<InputType, string> Input = new Dictionary<InputType, string>()
        {
            {InputType.Keyboard, $"Prefabs/Input/InputView_{InputType.Keyboard}" },
        };
        
        public static string UiAssetBundleUrl = _googleDiskUrl+ "1cfqLgbXMdAGx9OmMVEE50AnF-wWcu8kU";
        public static string AudioAssetBundleUrl =  _googleDiskUrl+ "1LVDwRLyrIVdP_dlFPB4DyY2BNRG3Avlr";
    }

}
