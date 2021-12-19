using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class PauseMenuView : MonoBehaviour
    {
        [field: SerializeField] public Button MainMenuButton { get; private set; }
        [field: SerializeField] public Button ContinueButton { get; private set; }

        public void Hide() => gameObject.SetActive(false);

        public void Show() => gameObject.SetActive(true);
    }
}