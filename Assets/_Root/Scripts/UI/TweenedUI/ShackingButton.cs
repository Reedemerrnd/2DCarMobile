using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.UI
{
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(Button))]
    internal class ShackingButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [field: SerializeField] public Button Button { get; private set; }


        [Header("Tween Settings")] 
        [SerializeField] private Ease _ease;
        [SerializeField] private float _duration;
        [SerializeField] private float _strength;
        [SerializeField] private int _vibrato;

        private Quaternion _originalRotation;
        private Tweener _animation;

        private void OnValidate()
        {
            InitComponents();
        }

        private void Awake()
        {
            InitComponents();
        }

        private void OnDestroy()
        {
            Stop();
        }

        private void InitComponents()
        {
            Button ??= GetComponent<Button>();
            _originalRotation = Button.transform.localRotation;
        }

        [ContextMenu("Play")]
        private void PlayAnimation()
        {
            _animation = Button.transform.DOShakeRotation(_duration, _strength * Vector3.forward, _vibrato).SetEase(_ease);
        }

        [ContextMenu("Stop")]
        private void Stop()
        {
            _animation?.Kill();
            Button.transform.localRotation = _originalRotation;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            PlayAnimation();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Stop();
        }
    }
}