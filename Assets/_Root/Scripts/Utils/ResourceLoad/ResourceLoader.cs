using UnityEngine;

namespace Game.Utils.ResourcesLoad
{
    internal class ResourceLoader : ITransportLoader, IUILoader
    {
        public GameObject Load(TransportType type) => Resources.Load<GameObject>(ResourcePath.Transport[type]);
        public GameObject Load(UIType type) => Resources.Load<GameObject>(ResourcePath.UI[type]);
    }

}
