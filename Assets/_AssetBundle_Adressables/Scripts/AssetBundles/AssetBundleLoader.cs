using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

namespace Game.Utils.ResourcesLoad.AssetBundles
{
    internal class AssetBundleLoader
    {
        private AssetBundle _spriteBundle;
        private AssetBundle _audioBUndle;

        private async Task<UnityWebRequest> LoadAssetBundleAsync(string url)
        {
            var request = UnityWebRequestAssetBundle.GetAssetBundle(url);
            request.SendWebRequest();
            while (!request.isDone)
            {
                await Task.Yield();
            }
            return request;
        }

        private async Task<AssetBundle> GetAssetBundleContentAsync(string url)
        {
            var source = await LoadAssetBundleAsync(url);

            if (source != null)
            {
                return DownloadHandlerAssetBundle.GetContent(source);
            }
            else
            {
                Debug.LogError("ErrorLoadingBundle");
                return null;
            }
        }

        public async Task<Sprite> GetSpriteAsync(string assetName)
        {
            if (_spriteBundle == null)
            {
                _spriteBundle = await GetAssetBundleContentAsync(ResourcePath.UiAssetBundleUrl);
            }

            return _spriteBundle.LoadAsset<Sprite>(assetName);
        }

        public async Task<AudioClip> GetAudioClipAsync(string assetName)
        {
            if (_audioBUndle == null)
            {
                _audioBUndle = await GetAssetBundleContentAsync(ResourcePath.AudioAssetBundleUrl);
            }

            return _audioBUndle.LoadAsset<AudioClip>(assetName);
        }
    }
}