using System;

namespace GameEvent
{
    public interface IGameDataEvent
    {
        //Action<bool, IGameEventView> ResolutionCb { get; }
        GameEventType EventType { get; }
        int ID { get; }
        bool RequirementsMetValidation();
    }
}
