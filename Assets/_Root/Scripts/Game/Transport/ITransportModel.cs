namespace Game.Transport
{
    internal interface ITransportModel
    {
        public float Speed { get; set; }
        public float JumpHeight { get; set; }
        public TransportType Type { get; }
    }
}