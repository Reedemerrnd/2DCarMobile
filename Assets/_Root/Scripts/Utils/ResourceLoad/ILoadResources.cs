using System;
using UnityEngine;

namespace Game.Utils
{
    internal interface ILoadResources<T> where T : Enum
    {
        public GameObject Load(T type);
        public K Spawn<K>(T type,Vector3 position, Quaternion rotation);
    }
}
