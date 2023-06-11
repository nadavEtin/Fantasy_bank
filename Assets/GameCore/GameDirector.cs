using GameCore.ScriptableObjects;
using GameCore.UI;
using Bank;
using GameCore.EventBus;
using GameCore.EventBus.GameplayEvents;
using GameEvent;
using UnityEngine;
using VContainer;

namespace GameCore
{
    public class GameDirector : IGameDirector
    {
        private readonly EventBus.EventBus _eventBus;

        private readonly IAssetRefs _assetRefs;
        private readonly Canvas _canvas;
        private readonly IGameEventManager _geManager;

        private readonly Camera _camera;
        private readonly IBankBalance _bankBalance;
        private readonly IUiManager _uiManager;

        public GameDirector(EventBus.EventBus bus, IAssetRefs assetRefs, IBankBalance bankBalance,
            IGameEventManager eventManager, Canvas canvas, Camera camera, IUiManager uiManager)
        {
            _assetRefs = assetRefs;
            _canvas = canvas;
            _camera = camera;
            _eventBus = bus;
            _bankBalance = bankBalance;
            _uiManager = uiManager;
            _geManager = eventManager;

            GameEventCreate();
        }
        

        #region Game Flow

        private void AdvanceTurn()
        {
            _eventBus.Publish(GameplayEvent.NextTurn, new NextTurnEventParams());
        }

        private void GameEventCreate()
        {
            _geManager.CreateGameEvent(GameEventType.Loan);
        }

        #endregion
    }
}