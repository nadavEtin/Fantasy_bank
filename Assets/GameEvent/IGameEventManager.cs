using GameEvent.LoanEvent;

namespace GameEvent
{
    public interface IGameEventManager
    {
        void CreateGameEvent(GameEventType type);
    }
}