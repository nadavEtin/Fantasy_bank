using System;
using GameEvent.LoanEvent;

namespace GameEvent
{
    public interface IGameDataEvent
    {
        public Action YesResult { get; }
        public Action NoResult { get; }
        public GameEventType Type { get; }
    }
}
