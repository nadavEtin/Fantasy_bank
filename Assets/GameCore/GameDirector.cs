using Assets.GameCore.GameFlow;
using Bank;
using DG.Tweening;
using GameCore.EventBus;
using GameCore.EventBus.GameplayEvents;
using GameCore.ScriptableObjects;
using GameCore.UI;
using GameEvent;
using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameCore
{
    public enum GamePhases
    {
        ResolveReadyEvents,
        NewTurn,        
        AdvanceEvent,
        EndPhase
    }

    public class GameDirector : IGameDirector, IStartable
    {
        private readonly EventsManager _eventsManager;

        private readonly IAssetRefs _assetRefs;
        private readonly IStoriesRefs _storyRefs;
        private readonly Canvas _canvas;
        private readonly IGameEventManager _geManager;

        private readonly Camera _camera;
        private readonly IBankBalance _bankBalance;
        private readonly IUiManager _uiManager;

        private PhaseManager _phaseManager;
        private GamePhases[] _gamePhases;
        private GamePhases _currentPhase;

        //test
        private readonly IObjectResolver _resolver;

        public GameDirector(EventsManager bus, IAssetRefs assetRefs, IStoriesRefs storyRefs, IBankBalance bankBalance,
            IGameEventManager eventManager, Canvas canvas, Camera camera, IUiManager uiManager, IObjectResolver resolver)
        {
            _assetRefs = assetRefs;
            _storyRefs = storyRefs;
            _canvas = canvas;
            _camera = camera;
            _eventsManager = bus;
            _bankBalance = bankBalance;
            _uiManager = uiManager;
            _geManager = eventManager;
            _gamePhases = (GamePhases[])Enum.GetValues(typeof(GamePhases));
            _currentPhase = _gamePhases[0];
            _phaseManager = new PhaseManager(_eventsManager);

            _resolver = resolver;
            ComponentsSetup();
            EventSubscriptionsSetup();
            StartGame();
            //GameEventCreate();
        }

        private void StartGame()
        {
            _eventsManager.Publish(GameplayEvent.GameStart, new EmptyParams());
            _currentPhase = GamePhases.ResolveReadyEvents;
            GamePhaseDone(_currentPhase);

        }

        private void ComponentsSetup()
        {
            DOTween.Init();
            _storyRefs.InitSetup();
        }

        private void EventSubscriptionsSetup()
        {
            _eventsManager.Subscribe(GameplayEvent.PhaseEnded, OnPhaseEnded);
        }


        #region Game Flow

        //TODO: maybe change to a list of phases? easier to dynamically change the order that way?
        public void GamePhaseDone(GamePhases phase)
        {
            switch (phase)
            {
                case GamePhases.ResolveReadyEvents:
                    _eventsManager.Publish(GameplayEvent.MainPhase, new EmptyParams());
                    break;
                case GamePhases.NewTurn:
                    _eventsManager.Publish(GameplayEvent.ShowNewStoryEvent, new EmptyParams());
                    break;
                
                case GamePhases.AdvanceEvent:
                    break;
                case GamePhases.EndPhase:
                    _eventsManager.Publish(GameplayEvent.ResolveReadyEvents, new EmptyParams());
                    break;
                default:
                    break;
            }
        }

        private void OnPhaseEnded(BaseEventParams eventParams)
        {
            var eventString = (SingleParamString)eventParams;
            var phaseName = eventString.Value;
            GamePhaseDone(_currentPhase);
        }

        private void AdvanceTurn()
        {
            _eventsManager.Publish(GameplayEvent.NextTurn, new NextTurnEventParams());
        }

        private void GameEventCreate()
        {


            _resolver.Instantiate(_assetRefs.EventResolutionScreen);
        }

        #endregion

        public void Start()
        {
            //THIS IS NEEDED TO CALL THIS OBJ'S CONSTRUCTOR AFTER REGISTERING IN LIFETIME SCOPE
        }

        
    }
}