using System;
using Bank;
using GameCore;
using GameCore.Events;
using GameCore.ScriptableObjects;
using GameEvent.LoanEvent;
using UnityEngine;

namespace GameEvent
{
    public interface IGameEventView
    {
        IGameDataEvent EventData { get; }
    }

    public class GameEventView : MonoBehaviour, IGameEventView
    {
        private bool _pressed;
        private IGameDataEvent _eventData;
        public IGameDataEvent EventData => _eventData;
        
        //remove these after setting up the constructor
        [SerializeField] private Camera _camera;
        [SerializeField] private GameEventSettings _settings;

        private TouchEventParams _curTouchData;
        private Vector2 _neutralPos;
        private IGameDirector _gameDirector;
        private IBankManager _bankManager;
        private IGameEventManager _gameEventManager;
        private Action<bool, IGameEventView> _resolutionCb;

        public void Init(IGameDirector gameDirector,  IBankManager bankManager, Action<bool, 
            IGameEventView> resolutionCb, Camera camera)
        {
            _camera = camera;
            _gameDirector = gameDirector;
            _bankManager = bankManager;
            _resolutionCb = resolutionCb;
            _eventData = new LoanGameEventData(1, "event text", "title", 
                resolutionCb, 100, 75, GameEventType.Loan, null);
        }

        public void ActivateEvent()
        {
            if (_bankManager.GoldBalance < _eventData.LoanPrice)
            {
                //TODO: show some 'not enough gold' message
                OnNoResult();
            }
            else
            {
                //TODO: show the event
            }
        }

        private void Update()
        {
            if (!_pressed)
                return;
            
            //update the touch data for this frame
            _curTouchData = _gameDirector.RecentTouch;
            
            //---------   TODO:  MAKE A UNIT TEST FOR THIS -------//
            transform.position = (Vector2)_camera.ScreenToWorldPoint(_curTouchData.TouchData.ScreenPosition);
            if (Mathf.Abs(transform.position.x) - _neutralPos.x > _settings.DragDistanceToResolveCard)
            {
                if(transform.position.x > _neutralPos.x)
                    OnYesResult();
                else
                    OnNoResult();
            }
            //--------                               --------//
        }

        private void OnMouseDown()
        {
            _pressed = true;
            _curTouchData = _gameDirector.RecentTouch;
        }

        private void OnMouseUp()
        {
            _pressed = false;
            SnapToNeutralPos();
        }

        private void SnapToNeutralPos()
        {
            transform.position = _neutralPos;
        }

        private void OnYesResult()
        {
            Debug.Log("yes result");
            _pressed = false;
            
            
            var res = _bankManager.GetGoldFromBank(_eventData.LoanPrice);
            if(res == false)
                OnNoResult();
                    
            //_gameEventManager.
                //TODO: continue process after approved loan
            
            _eventData.ResolutionCb(true, this);
            
            //temp
            SnapToNeutralPos();
        }

        private void OnNoResult()
        {
            Debug.Log("no result");
            _pressed = false;
            _eventData.ResolutionCb(false, this);
            
            //temp
            SnapToNeutralPos();
        }
    }
}
