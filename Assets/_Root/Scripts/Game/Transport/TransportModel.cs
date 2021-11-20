namespace Game.Models
{
    internal class TransportModel
    {
        private readonly TransportType _type;
        private float _speed;
        private float _jumpHeight;

        public float JumpHeight => _jumpHeight;
        public float Speed => _speed;
        public TransportType Type => _type;


        public TransportModel(TransportType type, float speed, float jumpHeight)
        {
            _type = type;
            _speed = speed;
            _jumpHeight = jumpHeight;
        }
    }
}
