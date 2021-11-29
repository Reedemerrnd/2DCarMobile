using UnityEngine;
using Game.Utils.ResourcesLoad;
using Game.Views;
using Game.Abilities;

namespace Game.Utils
{
    internal class ResourceLoader : IResourceLoader
    {
        public GameObject Load(TransportType type) => Resources.Load<GameObject>(ResourcePath.Transport[type]);
        public K Spawn<K>(TransportType type, Vector3 position, Quaternion rotation)
        {
            var prefab = Load(type);
            return SpawnAndGetComponent<K>(prefab, position, rotation);
        }


        public GameObject Load(UIType type) => Resources.Load<GameObject>(ResourcePath.UI[type]);
        public K Spawn<K>(UIType type, Vector3 position, Quaternion rotation)
        {
            var prefab = Load(type);
            return SpawnAndGetComponent<K>(prefab, position, rotation);
        }
       

        public GameObject Load(InputType type) => Resources.Load<GameObject>(ResourcePath.Input[type]);
        public K Spawn<K>(InputType type, Vector3 position, Quaternion rotation)
        {
            var prefab = Load(type);
            return SpawnAndGetComponent<K>(prefab, position, rotation);
        }


        public LevelBackgroundView LoadLevel(int index)
        {
            var prefab = Resources.Load<GameObject>($"Prefabs/Levels/Level{index}");
            return SpawnAndGetComponent<LevelBackgroundView>(prefab, Vector3.zero, Quaternion.identity);
        }

        public AbilitiesData LoadAbilitiesData() => Resources.Load<AbilitiesData>("Abilities/AbilitiesData");

        private K SpawnAndGetComponent<K>(GameObject obj, Vector3 position, Quaternion rotation)
        {
            var gameObj = Object.Instantiate(obj, position, rotation);

            return gameObj.GetComponent<K>();
        }

    }
}
