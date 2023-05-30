using System;
using GameEvent.LoanEvent;

namespace GameEvent
{
    public interface IGameDataEvent
    {
        Action<bool, IGameEventView> ResolutionCb { get; }
        GameEventType Type { get; }
        int LoanPrice { get; }
    }
}
