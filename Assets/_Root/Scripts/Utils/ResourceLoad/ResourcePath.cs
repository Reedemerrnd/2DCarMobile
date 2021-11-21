using System.Collections.Generic;

namespace Game.Utils.ResourcesLoad
{
    internal static class ResourcePath
    {
        public static Dictionary<TransportType, string> Transport = new Dictionary<TransportType, string>()
        {
            {TransportType.Car, $"Prefabs/Transport/{TransportType.Car}" },
            {TransportType.Boat, $"Prefabs/Transport/{TransportType.Boat}" }
        };

        public static Dictionary<UIType, string> UI = new Dictionary<UIType, string>()
        {
            {UIType.MainMenu, $"Prefabs/UI/{UIType.MainMenu}" },
            {UIType.SettingsMenu, $"Prefabs/UI/{UIType.SettingsMenu}" },
            {UIType.InGame, $"Prefabs/UI/{UIType.InGame}" }
        };

        public static Dictionary<InputType, string> Input = new Dictionary<InputType, string>()
        {
            {InputType.Keyboard, $"Prefabs/Input/InputView_{InputType.Keyboard}" },
        };
    }

}
