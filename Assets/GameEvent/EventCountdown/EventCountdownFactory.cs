using GameCore.ScriptableObjects;
using GameCore.Utility.GeneralClasses;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameEvent.EventCountdown
{
    public class EventCountdownFactory : BaseFactory
    {
        //private readonly IAssetRefs _assetRefs;
        //private IObjectResolver _resolver;

        public EventCountdownFactory(IAssetRefs assetRefs, IObjectResolver resolver) : base(assetRefs, resolver)
        {

        }

        public override GameObject Create()
        {
            return _resolver.Instantiate(_assetRefs.EventCountdown);
            //return GameObject.Instantiate(_assetRefs.EventCountdown).GetComponent<EventCountdownView>();
        }

        public override GameObject Create(Transform parent)
        {
            return _resolver.Instantiate(_assetRefs.EventCountdown, parent);
            //return GameObject.Instantiate(_assetRefs.EventCountdown).GetComponent<EventCountdownView>();
        }
    }
}