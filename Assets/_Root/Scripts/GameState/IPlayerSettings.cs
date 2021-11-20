namespace Game.Models
{
    internal interface IPlayerSettings
    {
        public TransportType TransportType { get; }
        public float Speed { get; }
        public float JumpHeight { get; }
    }
}
