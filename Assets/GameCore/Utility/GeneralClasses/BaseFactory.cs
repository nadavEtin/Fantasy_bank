using Assets.GameCore.Utility.ObjectPool;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameCore.Utility.GeneralClasses
{
    public abstract class BaseFactory : IBaseFactory
    {
        //protected IAssetRefs _assetRefs;
        protected IObjectResolver _resolver;
        protected ISingleObjectPool _factoryObjectPool;
        protected GameObject _prefabGameObj;

        protected BaseFactory(/*IAssetRefs assetRefs,*/ IObjectResolver resolver)
        {
            //_assetRefs = assetRefs;
            _resolver = resolver;
        }

        public virtual GameObject Create()
        {
            var newObj = _factoryObjectPool.GetObjectFromPool();

            //Pool is empty
            if (newObj == null)
            {
                newObj = _resolver.Instantiate(_prefabGameObj);
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