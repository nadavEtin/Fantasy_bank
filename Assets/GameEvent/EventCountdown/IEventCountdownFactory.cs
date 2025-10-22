using UnityEngine;

namespace GameEvent.EventCountdown
{
    public interface IEventCountdownFactory
    {
        GameObject Create(Transform transform = null);
    }
}