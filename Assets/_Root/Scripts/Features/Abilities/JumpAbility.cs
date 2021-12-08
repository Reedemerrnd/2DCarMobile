using Game.Transport;
using UnityEngine;

namespace Game.Abilities
{
    internal sealed class JumpAbility : ActiveAbility
    {
        
        public JumpAbility(ActiveAbilityInfo abilityInfo) : base(abilityInfo)
        {
        }

        public override void Apply(ITransportView target, ITransportModel model)
        {
            target.Rigidbody.AddForce(Vector2.up * _value * model.JumpHeight);
        }
    }
}