using Assets.GameCore.Utility.ObjectPool;
using GameCore.ScriptableObjects;
using GameCore.Utility.GeneralClasses;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameEvent.EventResolution
{
    public class EventResolutionViewFactory : BaseFactory
    {
        //private GameObject _eventResolutionPrefab;
        //private ISingleObjectPool _eventResolutionPool;

        public EventResolutionViewFactory(IAssetRefs assetRefs, IObjectResolver resolver) : base(/*assetRefs,*/ resolver)
        {
            //_eventResolutionPrefab = _assetRefs.EventResolutionScreen;
            _factoryObjectPool = new SingleObjectPool();
            _prefabGameObj = assetRefs.EventResolutionScreen;
        }

        /*public override GameObject Create()
        {
            var eventResolution = _eventResolutionPool.GetObjectFromPool();

            //Pool is empty
            if (eventResolution == null)
                return eventResolution = _resolver.Instantiate(_assetRefs.EventResolutionScreen);
            else
                return eventResolution;
        }*/

        /*public override GameObject Create(Transform parent)
        {
            var eventResolution = _eventResolutionPool.GetObjectFromPool();

            if (eventResolution == null)
                return eventResolution = _resolver.Instantiate(_assetRefs.EventResolutionScreen, parent);
            else
            {
                eventResolution.transform.SetParent(parent);
                return eventResolution;
            }
        }

        public override void ReturnToObjectPool(GameObject obj)
        {
            _eventResolutionPool.AddObjectToPool(obj);
        }*/
    }
}