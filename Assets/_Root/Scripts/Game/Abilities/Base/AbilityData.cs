using System;
using UnityEngine;

namespace Game.Abilities
{
    internal abstract class AbilityData<T> : ScriptableObject, IAbilityInfo where T : Enum
    {
        [field: SerializeField] public string ID { get; private set; }
        [field: SerializeField] public T Type { get; private set; }
        [field: SerializeField] public float Value { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public GameObject Prefab { get; private set; }
    }
}
