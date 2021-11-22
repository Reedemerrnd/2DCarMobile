using UnityEngine;

namespace Game.Models
{
    [CreateAssetMenu(menuName = "Game/GameSettings")]
    internal class GameSettings : ScriptableObject
    {
        [field: SerializeField] public GameState State { get; private set; }

        [field: SerializeField] public TransportType TransportType { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float JumpHeight { get; private set; }
        [field: SerializeField] public InputType Input { get; private set; }
    }
}
