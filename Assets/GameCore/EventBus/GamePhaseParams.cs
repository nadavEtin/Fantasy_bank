using GameCore;
using GameCore.EventBus;

namespace Assets.GameCore.EventBus
{
    public class GamePhaseParams : BaseEventParams
    {
        public GamePhases PhaseName { get; private set; }
        public GamePhaseParams(GamePhases phaseName)
        {
            PhaseName = phaseName;
        }
    }
}
