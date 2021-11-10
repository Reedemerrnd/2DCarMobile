using System.Collections.Generic;

namespace Game.Utils.ResourcesLoad
{
    internal static class ResourcePath
    {
        public static Dictionary<TransportType, string> Transport = new Dictionary<TransportType, string>()
        {
            {TransportType.Car, $"Resources/{TransportType.Car}" },
            {TransportType.Boat, $"Resources/{TransportType.Boat}" }
        };

        public static Dictionary<UIType, string> UI = new Dictionary<UIType, string>()
        {
            {UIType.MainMenu, $"Resources/{UIType.MainMenu}" },
            {UIType.Settings, $"Resources/{UIType.Settings}" }
        };
    }

}
