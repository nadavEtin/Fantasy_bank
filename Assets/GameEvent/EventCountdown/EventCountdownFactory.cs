using GameCore.ScriptableObjects;
using GameCore.Utility.GeneralClasses;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameEvent.EventCountdown
{
    public class EventCountdownFactory : BaseFactory
    {
        public EventCountdownFactory(IAssetRefs assetRefs, IObjectResolver resolver) : base(assetRefs, resolver)
        {

        }

        public override GameObject Create()
        {
            return _resolver.Instantiate(_assetRefs.EventCountdown);
        }

        public override GameObject Create(Transform parent)
        {
            return _resolver.Instantiate(_assetRefs.EventCountdown, parent);
        }
    }
}