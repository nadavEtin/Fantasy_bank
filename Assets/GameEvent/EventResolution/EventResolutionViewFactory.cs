using GameCore.ScriptableObjects;
using GameCore.Utility.GeneralClasses;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameEvent.EventResolution
{
    public class EventResolutionViewFactory : BaseFactory
    {
        public EventResolutionViewFactory(IAssetRefs assetRefs, IObjectResolver resolver) : base(assetRefs, resolver) { }

        public override GameObject Create()
        {
            return _resolver.Instantiate(_assetRefs.EventResolutionScreen);
        }

        public override GameObject Create(Transform parent)
        {
            return _resolver.Instantiate(_assetRefs.EventResolutionScreen);
        }

        
    }
}