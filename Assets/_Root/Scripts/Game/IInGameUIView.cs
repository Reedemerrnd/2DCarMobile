using Game.Abilities;
using UnityEngine.UI;

namespace Game
{
    internal interface IInGameUIView
    {
        Button ActiveAbilityButton { get; }
        Button StartFightButton { get; }
        void InitAbility(IAbilityInfo abilityInfo);
    }
}