using System;

namespace Assets.GameCore.GameFlow
{
    public interface IPhaseProcess
    {
        string Name { get; }
        bool IsComplete { get; }
        event Action OnCompleted;
        //void StartProcess();
    }
}