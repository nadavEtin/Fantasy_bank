using Assets.GameCore.Utility.ObjectPool;
using GameCore.ScriptableObjects;
using GameCore.Utility.GeneralClasses;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameEvent.EventCountdown
{
    public class EventCountdownFactory : BaseFactory
    {
        //private ISingleObjectPool _countdownObjectPool;

        public EventCountdownFactory(IAssetRefs assetRefs, IObjectResolver resolver) : base(/*assetRefs,*/ resolver)
        {
            _factoryObjectPool = new SingleObjectPool();
            _prefabGameObj = assetRefs.EventCountdown;
        }

        /*public override GameObject Create()
        {

            return _resolver.Instantiate(_assetRefs.EventCountdown);
        }*/

       /* public override GameObject Create(Transform parent)
        {
            return _resolver.Instantiate(_assetRefs.EventCountdown, parent);
        }*/

        /*public override void ReturnToObjectPool(GameObject obj)
        {
            _factoryObjectPool.AddObjectToPool(obj);
        }*/
    }
}