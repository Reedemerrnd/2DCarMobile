using System;
using UnityEngine;

namespace Game.Utils.ResourcesLoad
{
    internal interface IResourceLoader<T> where T : Enum
    {
        public GameObject Load(T type);
    }

}
