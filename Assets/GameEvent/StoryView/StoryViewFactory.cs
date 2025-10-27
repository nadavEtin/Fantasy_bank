using Assets.GameCore.Utility.ObjectPool;
using GameCore.ScriptableObjects;
using GameCore.Utility.GeneralClasses;
using UnityEngine;
using VContainer;

namespace Assets.GameEvent.EventCardView
{
    public class StoryViewFactory : BaseFactory
    {
        public StoryViewFactory()
        {
            _factoryObjectPool = new SingleObjectPool();
            _prefabGameObj = _assetRefs.StoryView;
        }
    }
}