using Assets.Scripts.ObjectPool;
using GameCore.ScriptableObjects;
using GameCore.Utility.GeneralClasses;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameEvent.EventResolution
{
    public class EventResolutionViewFactory : BaseFactory
    {
        private GameObject _eventResolutionPrefab;
        private ISingleObjectPool _eventResolutionPool;

        public EventResolutionViewFactory(IAssetRefs assetRefs, IObjectResolver resolver) : base(assetRefs, resolver)
        {
            _eventResolutionPrefab = assetRefs.EventResolutionScreen;
            _eventResolutionPool = new SingleObjectPool();
        }

        public override GameObject Create()
        {
            var eventResolution = _eventResolutionPool.GetObjectFromPool();

            if (eventResolution == null)
                return eventResolution = _resolver.Instantiate(_eventResolutionPrefab);
            else
                return eventResolution;

        }

        public override GameObject Create(Transform parent)
        {
            var eventResolution = _eventResolutionPool.GetObjectFromPool();

            if (eventResolution == null)            
                return eventResolution = _resolver.Instantiate(_eventResolutionPrefab, parent);
            else
            {
                eventResolution.transform.SetParent(parent);
                return eventResolution;
            }
        }
    }
}