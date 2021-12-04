using Game.Abilities;
using UnityEngine.UI;

namespace Game
{
    internal interface IInGameUIView
    {
        Button ActiveAbilityButton { get; }

        void InitAbility(IAbilityInfo abilityInfo);
    }
}