using Assets.GameCore.Utility.ObjectPool;
using GameCore.ScriptableObjects;
using GameCore.Utility.GeneralClasses;
using GameEvent.StoryView;
using UnityEngine;
using VContainer;

namespace Assets.GameEvent.EventCardView
{
    public class StoryViewFactory : BaseFactory, IStoryCardViewFactory
    {
        public StoryViewFactory(IAssetRefs assetRefs, IObjectResolver resolver) : base(/*assetRefs,*/ resolver)
        {
            _factoryObjectPool = new SingleObjectPool();
            _prefabGameObj = assetRefs.StoryView;
        }

        public IStoryCardView CreateNewStoryCardView(Transform parent = null)
        {
            var newStory = base.Create(parent);
            return newStory.GetComponent<IStoryCardView>();
        }
    }
}