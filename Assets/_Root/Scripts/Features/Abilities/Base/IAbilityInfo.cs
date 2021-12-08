using UnityEngine;

namespace Game.Abilities
{
    internal interface IAbilityInfo
    {
        string ID { get; }
        Sprite Icon { get; }
    }
}
