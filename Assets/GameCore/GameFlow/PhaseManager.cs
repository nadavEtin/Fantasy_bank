using Assets.GameCore.EventBus;
using GameCore;
using GameCore.EventBus;
using System;
using System.Collections.Generic;

namespace Assets.GameCore.GameFlow
{
    public class PhaseManager
    {
        private List<IPhaseProcess> _activePhaseProcesses;
        private GamePhases _activePhaseName;
        //private int _currentPhaseIndex;
        private EventsManager _eventsManager;

        public PhaseManager(EventsManager eventsManager)
        {
            //_currentPhaseIndex = 0;
            _eventsManager = eventsManager;
            _activePhaseProcesses = new List<IPhaseProcess>();
            _eventsManager.Subscribe(GameplayEvent.PhaseProcessStarted, OnPhaseProcessStarted);
            _eventsManager.Subscribe(GameplayEvent.PhaseProcessEnded, OnPhaseProcessEnded);
            _eventsManager.Subscribe(GameplayEvent.PhaseStarted, OnPhaseStarted);
        }

        private void OnPhaseStarted(BaseEventParams baseEventParams)
        {
            var phaseParams = (GamePhaseParams)baseEventParams;
            _activePhaseName = phaseParams.PhaseName;
        }

        private void OnPhaseEnded()
        {
            _eventsManager.Publish(GameplayEvent.PhaseEnded, new GamePhaseParams(_activePhaseName));
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
                OnPhaseEnded();
        }
    }
}
