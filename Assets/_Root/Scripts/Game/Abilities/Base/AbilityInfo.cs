namespace Game.Abilities
{
    internal struct AbilityInfo<T>
    {
        public string ID { get; private set; }
        public T Type { get; private set; }
    }
}
