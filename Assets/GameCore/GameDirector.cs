using Bank;
using DG.Tweening;
using GameCore.EventBus;
using GameCore.EventBus.GameplayEvents;
using GameCore.ScriptableObjects;
using GameCore.UI;
using GameEvent;
using Reflex.Attributes;
using System;
using UnityEngine;

namespace GameCore
{
    public enum GamePhases
    {
        ResolveReadyEvents,
        MainPhase,
        EndPhase,
        AdvanceEvent
    }

    public class GameDirector : IGameDirector
    {
        [Inject] private EventBus.EventBus _eventBus;
        [Inject] private IAssetRefs _assetRefs;
        [Inject] private IStoriesRefs _storyRefs;
        [Inject] private Canvas _canvas;
        [Inject] private IGameEventManager _geManager;
        [Inject] private Camera _camera;
        [Inject] private IBankBalance _bankBalance;
        [Inject] private IUiManager _uiManager;
        //[Inject] private IObjectResolver _resolver;

        private GamePhases[] _gamePhases;
        private GamePhases _currentPhase;

        //test
        //private readonly IObjectResolver _resolver;

        public GameDirector()
        {            
            _gamePhases = (GamePhases[])Enum.GetValues(typeof(GamePhases));
            _currentPhase = _gamePhases[0];
            Debug.Log(_assetRefs.ToString());
            ComponentsSetup();
            StartGame();
            //GameEventCreate();
        }

        public void PubStartGame()
        {
            StartGame();
        }

        private void StartGame()
        {
            Debug.Log("Game Started");
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

        //TODO: maybe change to a list of phases? easier to dynamically change the order that way?
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


            //_resolver.Instantiate(_assetRefs.EventResolutionScreen);
        }

        #endregion

        public void Start()
        {
            //THIS IS NEEDED TO CALL THIS OBJ'S CONSTRUCTOR AFTER REGISTERING IN LIFETIME SCOPE
        }

        
    }
}