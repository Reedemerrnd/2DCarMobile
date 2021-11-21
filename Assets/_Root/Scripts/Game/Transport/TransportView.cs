using UnityEngine;

namespace Game.Transport
{
    [RequireComponent(typeof(Rigidbody2D))]
    internal abstract class TransportView : MonoBehaviour, ITransportView
    {
        private Rigidbody2D _rigidbody;

        public Rigidbody2D Rigidbody
        {
            get
            {
                if(_rigidbody == null)
                {
                    _rigidbody = GetComponent<Rigidbody2D>();
                }
                return _rigidbody;
            }
        }
    }
}
