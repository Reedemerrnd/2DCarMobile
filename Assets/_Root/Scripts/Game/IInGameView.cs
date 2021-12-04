using Game.Abilities;
using UnityEngine.UI;

namespace Game
{
    internal interface IInGameView
    {
        Button ActiveAbilityButton { get; }

        void InitAbility(IAbilityInfo abilityInfo);
    }
}