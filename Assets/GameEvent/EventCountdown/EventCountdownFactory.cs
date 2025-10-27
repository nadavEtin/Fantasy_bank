using Assets.GameCore.Utility.ObjectPool;
using GameCore.Utility.GeneralClasses;
using Reflex.Attributes;

namespace GameEvent.EventCountdown
{
    public class EventCountdownFactory : BaseFactory, IEventCountdownFactory
    {
        //[Inject] private IAssetRefs _assetRefs;

        public EventCountdownFactory()
        {
            _factoryObjectPool = new SingleObjectPool();
            _prefabGameObj = _assetRefs.EventCountdown;
        }
    }
}