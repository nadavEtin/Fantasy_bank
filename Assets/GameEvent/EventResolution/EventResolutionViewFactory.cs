using Assets.GameCore.Utility.ObjectPool;
using GameCore.ScriptableObjects;
using GameCore.Utility.GeneralClasses;
using VContainer;

namespace GameEvent.EventResolution
{
    public class EventResolutionViewFactory : BaseFactory
    {
        //[Inject] private IAssetRefs _assetRefs;

        public EventResolutionViewFactory()
        {
            _factoryObjectPool = new SingleObjectPool();
            _prefabGameObj = _assetRefs.EventResolutionScreen;
        }
    }
}