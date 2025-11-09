using System;

namespace Assets.GameCore.GameFlow
{
    public class PhaseProcess : IPhaseProcess
    {
        public string Name => _name;

        public bool IsComplete => _isComplete;
        public event Action OnCompleted;

        private string _name;
        private bool _isComplete;

        public PhaseProcess(string name, bool isComplete = false)
        {
            _name = name;
            _isComplete = false;
            //OnCompleted = onCompleted;
        }
    }
}
