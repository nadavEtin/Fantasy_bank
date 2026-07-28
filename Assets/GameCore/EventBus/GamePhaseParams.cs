using GameCore;
using GameCore.EventBus;

namespace Assets.GameCore.EventBus
{
    public class GamePhaseParams : BaseEventParams
    {
        public GamePhases PhaseType { get; private set; }
        public GamePhaseParams(GamePhases phaseName)
        {
            PhaseType = phaseName;
        }
    }
}
