using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.Pool
{
    public class Pool<PoolObj> where PoolObj : PoolObject
    {
        private List<PoolObj> _poolObjects;

        public Pool(PoolObj poolObjectPrefab, int capacity)
        {
            _poolObjects = new List<PoolObj>(capacity);
            InitPool(capacity, poolObjectPrefab);
        }

        private void InitPool(int capacity, PoolObj prefab)
        {
            for (int _ = 0; _ < capacity; _++)
                CreateElement(prefab);
        }

        private void CreateElement(PoolObj prefab)
        {
            PoolObj obj = GameObject.Instantiate(prefab);
            obj.gameObject.SetActive(false);

            _poolObjects.Add(obj);
        }

        public PoolObj GetObject(bool isActive = true)
        {
            for (int poolObject = 0; poolObject < _poolObjects.Capacity; poolObject++)
            {
                if (_poolObjects[poolObject].gameObject.activeInHierarchy)
                    continue;

                _poolObjects[poolObject].gameObject.SetActive(isActive);
                return _poolObjects[poolObject];
            }

            return null;
        }
    }
}
