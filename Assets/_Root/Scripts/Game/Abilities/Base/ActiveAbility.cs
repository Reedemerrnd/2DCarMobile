using UnityEngine;

namespace Game.Abilities
{
    internal abstract class ActiveAbility : Ability<GameObject>
    {
        public ActiveAbilityType Type { get; protected set; }


        public ActiveAbility(float value) : base(value)
        {

        }
    }
}
