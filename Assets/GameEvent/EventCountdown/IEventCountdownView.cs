using System;
using UnityEngine;

namespace GameEvent.EventCountdown
{
    public interface IEventCountdownView
    {
        void Setup(IGameDataEvent eventData);
        int CountdownDuration { get; }
        RectTransform ObjTransform { get; }
        //int Id { get; }
        IGameDataEvent EventData { get; }
        void ReduceCountdown(int amount);
        void CountdownDone();
    }
}