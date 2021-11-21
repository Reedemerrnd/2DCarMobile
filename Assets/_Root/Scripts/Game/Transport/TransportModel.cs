namespace Game.Transport
{
    internal class TransportModel : ITransportModel
    {
        public float JumpHeight { get; set; }
        public float Speed { get; set; }
        public TransportType Type { get; set; }


        public TransportModel(TransportType type, float speed, float jumpHeight)
        {
            Type = type;
            Speed = speed;
            JumpHeight = jumpHeight;
        }
    }
}
