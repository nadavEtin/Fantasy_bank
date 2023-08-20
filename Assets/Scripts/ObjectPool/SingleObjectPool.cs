using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ObjectPool
{
    public class SingleObjectPool : ISingleObjectPool
    {
        private readonly List<GameObject> _objectPool;

        public SingleObjectPool()
        {
            _objectPool = new List<GameObject>();
        }

        public void AddObjectToPool(GameObject obj)
        {
            obj.SetActive(false);
            _objectPool.Add(obj);
        }

        public GameObject GetObjectFromPool()
        {
            if (_objectPool.Count > 0)
            {
                var gameObj = _objectPool[0];
                _objectPool.RemoveAt(0);
                gameObj.SetActive(true);
                return gameObj;
            }

            return null;
        }
    }
}
