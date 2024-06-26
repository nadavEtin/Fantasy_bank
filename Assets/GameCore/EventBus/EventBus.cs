﻿using System;
using System.Collections.Generic;

namespace GameCore.EventBus
{
    public enum GameplayEvent
    {
        //input
        TouchStarted, TouchEnded,
        
        //game flow
        NextTurn, NextRound, ResolveReadyEvents, MainPhase,
        
        GoldBalanceChanged, EventApproved, EventCountdownDone,
        GameStart, GameEnd
    }

    // ReSharper disable once ClassNeverInstantiated.Global
    public class EventBus
    {
        private readonly Dictionary<GameplayEvent, List<Action<BaseEventParams>>> _subscription = new();

        public void Subscribe(GameplayEvent eventType, Action<BaseEventParams> handler)
        {
            if(_subscription.ContainsKey(eventType) == false) 
                _subscription.Add(eventType, new List<Action<BaseEventParams>>());

            var handlerList = _subscription[eventType];
            if (handlerList.Contains(handler) == false)
                handlerList.Add(handler);
        }

        public void Unsubscribe(GameplayEvent eventType, Action<BaseEventParams> handler)
        {
            if(_subscription.ContainsKey(eventType))
                _subscription[eventType]?.Remove(handler);
        }

        public void Publish(GameplayEvent eventType, BaseEventParams eventParams)
        {
            if (_subscription.ContainsKey(eventType) == false)
                return;

            var handlerList = _subscription[eventType];
            foreach (var handler in handlerList)
                handler?.Invoke(eventParams);
        }
    }
}
