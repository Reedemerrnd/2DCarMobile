using UnityEngine;

namespace Game.Abilities
{
    internal abstract class Ability<T>
    {
        protected float _value;
        public Sprite Icon { get; protected set; }
        public abstract void Apply(T target, float valueMod = 1f);
    }
}
