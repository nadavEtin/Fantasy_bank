using Assets.GameCore.Utility.ObjectPool;
using GameCore.ScriptableObjects;
using Reflex.Attributes;
using UnityEngine;

namespace GameCore.Utility.GeneralClasses
{
    public abstract class BaseFactory : IBaseFactory
    {
        [Inject] protected IAssetRefs _assetRefs;
        //protected GameObject _resolver;
        protected ISingleObjectPool _factoryObjectPool;
        protected GameObject _prefabGameObj;

        public virtual GameObject Create()
        {
            var newObj = _factoryObjectPool.GetObjectFromPool();

            //Pool is empty
            if (newObj == null)
            {
                newObj = Object.Instantiate(_prefabGameObj);
                try
                {
                    newObj.GetComponent<IPoolable>().SetupReturnToPoolCb(ReturnToObjectPool);
                }
                catch (System.Exception)
                {
                    Debug.LogError("New object prefab is missing IPoolable");
                    throw;
                }
            }

            return newObj;
        }

        public virtual GameObject Create(Transform parent)
        {
            var newObj = Create();
            newObj.transform.SetParent(parent);
            return newObj;
        }

        public virtual void ReturnToObjectPool(GameObject obj)
        {
            _factoryObjectPool.AddObjectToPool(obj);
        }
    }
}