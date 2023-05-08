using System;

namespace Assets.GameEvent
{
    public interface IGameDataEvent
    {
        public Action YesResult { get; }
        public Action NoResult { get; }
    }
}
