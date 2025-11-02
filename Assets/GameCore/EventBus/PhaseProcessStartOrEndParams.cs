using Assets.GameCore.GameFlow;
using GameCore.EventBus;

namespace Assets.GameCore.EventBus
{
    public class PhaseProcessStartOrEndParams : BaseEventParams
    {
        public IPhaseProcess PhaseProcess { get; private set; }

        public PhaseProcessStartOrEndParams(IPhaseProcess phaseProcessName)
        {
            PhaseProcess = phaseProcessName;
        }
    }
}
