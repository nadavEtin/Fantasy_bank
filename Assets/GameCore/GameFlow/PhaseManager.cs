using Assets.GameCore.EventBus;
using GameCore.EventBus;
using System;
using System.Collections.Generic;

namespace Assets.GameCore.GameFlow
{
    public class PhaseManager
    {
        private List<IPhaseProcess> _activePhaseProcesses;
        private string _activePhaseName;
        private int _currentPhaseIndex;
        private EventsManager _eventBus;

        public PhaseManager(EventsManager eventBus)
        {
            _currentPhaseIndex = 0;
            _eventBus = eventBus;
            _eventBus.Subscribe(GameplayEvent.PhaseProcessStarted, OnPhaseProcessStarted);
            _eventBus.Subscribe(GameplayEvent.PhaseProcessEnded, OnPhaseProcessEnded);
            eventBus.Subscribe(GameplayEvent.PhaseStarted, OnPhaseStarted);
        }

        private void OnPhaseStarted(BaseEventParams baseEventParams)
        {
            var phaseParams = (SingleParamString)baseEventParams;
            _activePhaseName = phaseParams.Value;
        }

        private void OnPhaseProcessStarted(BaseEventParams baseEventParams)
        {
            var phaseParams = (PhaseProcessStartOrEndParams)baseEventParams;
            _activePhaseProcesses.Add(phaseParams.PhaseProcess);
            //_currentPhaseIndex = 0;
            //StartNextPhase();
        }

        private void OnPhaseProcessEnded(BaseEventParams baseEventParams)
        {
            var phaseParams = (PhaseProcessStartOrEndParams)baseEventParams;
            var endedPhase = phaseParams.PhaseProcess;
            if (_activePhaseProcesses.Contains(endedPhase))
                _activePhaseProcesses.Remove(endedPhase);
            else
                UnityEngine.Debug.LogWarning($"{endedPhase.Name} not found in process list");
            //_currentPhaseIndex++;
            if (_activePhaseProcesses.Count == 0)
                StartNextPhase();
        }

        public void StartNextPhase()
        {
            //var currentPhase = _phaseProcesses[_currentPhaseIndex];
            //currentPhase.OnCompleted += OnPhaseCompleted;
            //currentPhase.StartProcess(); // Uncomment when StartProcess is implemented
            _eventBus.Publish(GameplayEvent.PhaseEnded, new EmptyParams());
            Console.WriteLine("All phases completed.");

        }
        private void OnPhaseCompleted()
        {
            //var completedPhase = _activePhaseProcesses[_currentPhaseIndex];
            //completedPhase.OnCompleted -= OnPhaseCompleted;
            //_currentPhaseIndex++;
            //StartNextPhase();
        }
    }
}
