namespace Game.Transport
{
    internal interface ITransportModel
    {
        public float Speed { get; }
        public float JumpHeight { get; }
        public TransportType Type { get; }
    }
}
