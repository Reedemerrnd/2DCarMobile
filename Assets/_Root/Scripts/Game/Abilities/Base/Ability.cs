namespace Game.Abilities
{
    internal abstract class Ability<T>
    {
        protected float _value;

        protected Ability(float value)
        {
            _value = value;
        }

        public abstract void Apply(T target);
    }
}
