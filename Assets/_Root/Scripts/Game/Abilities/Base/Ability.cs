namespace Game.Abilities
{
    internal abstract class Ability<TView, TModel>
    {
        protected float _value;
        public abstract void Apply(TView view, TModel model);
    }
}