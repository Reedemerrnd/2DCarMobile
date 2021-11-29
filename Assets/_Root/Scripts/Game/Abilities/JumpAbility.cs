using Game.Transport;
using UnityEngine;

namespace Game.Abilities
{
    internal sealed class JumpAbility : ActiveAbility
    {
        public JumpAbility(ActiveAbilityInfo abilityInfo) : base(abilityInfo)
        {
        }

        public override void Apply(ITransportView target, float valueMod = 1)
        {
            target.Rigidbody.AddForce(Vector2.up * _value * valueMod);
        }
    }

}
