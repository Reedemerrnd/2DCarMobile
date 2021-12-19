using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Utils.ResourcesLoad.AssetBundles
{
    [Serializable]
    internal class DataSpriteBundle
    {
        [field: SerializeField] public string NameAssetBundle { get; private set; }
        [field: SerializeField] public Image Image { get; private set; }
    }
}