using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    internal class MainMenuView : MonoBehaviour
    {
        [field: SerializeField] public Button StartButton { get; private set; }
        [field: SerializeField] public Button SettingsButton { get; private set; }
        [field: SerializeField] public Button RewardAdtButton { get; private set; }
        [field: SerializeField] public Button BuyButton { get; private set; }
        [field: SerializeField] public Button GarageButton { get; private set; }
    }
}