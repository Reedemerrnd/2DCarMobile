namespace Game.Models
{
    public class TransportModel
    {
        private float _speed;

        public float Speed => _speed;


        public TransportModel(float speed)
        {
            _speed = speed;
        }
    }
}
