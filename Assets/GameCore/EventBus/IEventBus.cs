using System;

namespace GameCore.EventBus
{
    public interface IEventBus
    {
        void Publish(GameplayEvent eventType, BaseEventParams eventParams);
        void Subscribe(GameplayEvent eventType, Action<BaseEventParams> handler);
        void Unsubscribe(GameplayEvent eventType, Action<BaseEventParams> handler);
    }
}