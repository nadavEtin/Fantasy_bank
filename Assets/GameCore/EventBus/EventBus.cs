using System;
using System.Collections.Generic;

namespace GameCore.Events
{
    public enum GameplayEvent
    {
        //input
        TouchStarted, TouchEnded,
        
        GoldBalanceChanged,
        GameStart, GameEnd
    }

    // ReSharper disable once ClassNeverInstantiated.Global
    public class EventBus
    {
        private readonly Dictionary<GameplayEvent, List<Action<BaseEventParams>>> _subscription = new();

        public void Subscribe(GameplayEvent eventType, Action<BaseEventParams> handler)
        {
            //var handlerList = _subscription[eventType];
            if(_subscription.ContainsKey(eventType) == false)
            //if (handlerList == null)
            {
                //handlerList = new List<Action<BaseEventParams>>();
                _subscription.Add(eventType, new List<Action<BaseEventParams>>());
            }

            var handlerList = _subscription[eventType];
            if (handlerList.Contains(handler) == false)
                handlerList.Add(handler);
        }

        public void Unsubscribe(GameplayEvent eventType, Action<BaseEventParams> handler)
        {
            _subscription[eventType]?.Remove(handler);
        }

        public void Publish(GameplayEvent eventType, BaseEventParams eventParams)
        {
            //var handlerList = _subscription[eventType];
            if (_subscription[eventType] == null)
                return;

            var handlerList = _subscription[eventType];
            foreach (var handler in handlerList)
                handler?.Invoke(eventParams);
        }
    }
}
