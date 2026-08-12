using GameEvent.StoryView;
using UnityEngine;

namespace Assets.GameEvent.EventCardView
{
    public interface IStoryCardViewFactory
    {
        IStoryCardView CreateNewStoryCardView(Transform parent = null);
    }
}