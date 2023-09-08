using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.GameCore.Utility.ObjectPool
{
    public enum ObjectTypes
    {
        GenericObject
    }

    public class MuktipleObjectPool : IMultipleObjectPool
    {
        private readonly Dictionary<ObjectTypes, List<GameObject>> _objectPool;

        public MuktipleObjectPool()
        {
            _objectPool = new Dictionary<ObjectTypes, List<GameObject>>();
        }

        public void AddObjectToPool(GameObject obj, ObjectTypes type)
        {
            if (_objectPool.ContainsKey(type) == false)
                _objectPool.Add(type, new List<GameObject>());

            obj.SetActive(false);
            _objectPool[type].Add(obj);
            obj.SetActive(false);
        }

        public GameObject GetObjectFromPool(ObjectTypes type)
        {
            if (_objectPool.ContainsKey(type) && _objectPool[type].Count > 0)
            {
                var returnObj = _objectPool[type][0];
                _objectPool[type].RemoveAt(0);
                returnObj.SetActive(true);
                return returnObj;
            }
            else
            {
                Debug.LogError("incorrect prefab type: " + Enum.GetName(typeof(ObjectTypes), type));
                return null;
            }
        }
    }
}
