using Bank;
using GameCore.EventBus;
using GameCore.Input;
using System;
using UnityEngine;

namespace GameEvent.StoryView
{
    public interface IStoryCardView
    {
        IGameDataEvent EventData { get; }
        void ActivateEvent(bool shouldActivate);
        void Init(IGameDataEvent storyData, Action<bool, IGameDataEvent> resolutionCb);
    }
}