using UnityEngine;

namespace Game.Transport
{
    interface ITransportView
    {
        public Rigidbody2D Rigidbody { get; }
    }
}
