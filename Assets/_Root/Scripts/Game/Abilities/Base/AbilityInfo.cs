using System;
using UnityEngine;

namespace Game.Abilities
{
    internal abstract class AbilityInfo<T> : ScriptableObject where T : Enum
    {
        [field: SerializeField] public T Type { get; private set; }
        [field: SerializeField] public float Value { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public GameObject Prefab { get; private set; }
    }
}
