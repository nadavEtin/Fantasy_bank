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
        MainPhase,
        EndPhase,
        AdvanceEvent
    }

    public class GameDirector : IGameDirector, IStartable
    {
        private readonly EventBus.EventBus _eventBus;

        private readonly IAssetRefs _assetRefs;
        private readonly IStoriesRefs _storyRefs;
        private readonly Canvas _canvas;
        private readonly IGameEventManager _geManager;

        private readonly Camera _camera;
        private readonly IBankBalance _bankBalance;
        private readonly IUiManager _uiManager;

        private GamePhases[] _gamePhases;
        private GamePhases _currentPhase;

        //test
        private readonly IObjectResolver _resolver;

        public GameDirector(EventBus.EventBus bus, IAssetRefs assetRefs, IStoriesRefs storyRefs, IBankBalance bankBalance,
            IGameEventManager eventManager, Canvas canvas, Camera camera, IUiManager uiManager, IObjectResolver resolver)
        {
            _assetRefs = assetRefs;
            _storyRefs = storyRefs;
            _canvas = canvas;
            _camera = camera;
            _eventBus = bus;
            _bankBalance = bankBalance;
            _uiManager = uiManager;
            _geManager = eventManager;
            _gamePhases = (GamePhases[])Enum.GetValues(typeof(GamePhases));
            _currentPhase = _gamePhases[0];

            _resolver = resolver;
            ComponentsSetup();
            StartGame();
            //GameEventCreate();
        }

        private void StartGame()
        {
            _eventBus.Publish(GameplayEvent.GameStart, new EmptyParams());
            //_currentPhase = GamePhases.ResolveReadyEvents;
            //GamePhaseDone(_currentPhase);

        }

        private void ComponentsSetup()
        {
            DOTween.Init();
            _storyRefs.InitSetup();
        }


        #region Game Flow

        public void GamePhaseDone(GamePhases phase)
        {
            switch (phase)
            {
                case GamePhases.ResolveReadyEvents:
                    _eventBus.Publish(GameplayEvent.MainPhase, new EmptyParams());
                    break;
                case GamePhases.MainPhase:
                    
                    break;
                
                case GamePhases.AdvanceEvent:
                    break;
                case GamePhases.EndPhase:
                    _eventBus.Publish(GameplayEvent.ResolveReadyEvents, new EmptyParams());
                    break;
                default:
                    break;
            }
        }

        private void AdvanceTurn()
        {
            _eventBus.Publish(GameplayEvent.NextTurn, new NextTurnEventParams());
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