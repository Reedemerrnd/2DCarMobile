using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace Game.Utils.ResourcesLoad.AssetBundles
{
    public class AssetBundleTestView : MonoBehaviour
    {
        [Header("AssetBundles")] [SerializeField]
        private Button _loadButton;
        [SerializeField] private string _spriteNameToLoad;
        [SerializeField] private string _audioNameToLoad;
        private AudioSource _audioSource;
        private AssetBundleLoader _loader;

        [Header("Addresables")] [SerializeField]
        private AssetReference _assetReference;
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private Button _loadImage;
        [SerializeField] private Button _unloadImage;

        private AsyncOperationHandle<Sprite> _addressablePrefab;


        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _loader = new AssetBundleLoader();

            _loadButton.onClick.AddListener(SetButtonImage);

            _loadImage.onClick.AddListener(LoadBackroundAsync);
            _unloadImage.onClick.AddListener(ResetBackground);
        }

        private async void SetButtonImage()
        {
            _loadButton.interactable = false;
            _loadButton.image.sprite = await _loader.GetSpriteAsync(_spriteNameToLoad);
            _audioSource.clip = await _loader.GetAudioClipAsync(_audioNameToLoad);

            _loadButton.interactable = true;
            _audioSource.Play();
        }

        private async void LoadBackroundAsync()
        {
            _addressablePrefab = Addressables.LoadAssetAsync<Sprite>(_assetReference);
            await _addressablePrefab.Task;

            _backgroundImage.sprite = _addressablePrefab.Result;
        }

        private void ResetBackground()
        {
            _backgroundImage.sprite = null;
            if (_addressablePrefab.Status == AsyncOperationStatus.Succeeded)
            {
                Addressables.Release(_addressablePrefab);
            }
        }


        private void OnDestroy()
        {
            _loadButton.onClick.RemoveListener(SetButtonImage);
            _loadImage.onClick.RemoveListener(LoadBackroundAsync);
            _unloadImage.onClick.RemoveListener(ResetBackground);

            if (_addressablePrefab.Status == AsyncOperationStatus.Succeeded)
            {
                Addressables.Release(_addressablePrefab);
            }
        }
    }
}