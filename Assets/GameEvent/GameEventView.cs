using System;
using Bank;
using GameCore;
using GameCore.Events;
using GameCore.Input;
using GameCore.ScriptableObjects;
using GameEvent.LoanEvent;
using UnityEngine;

namespace GameEvent
{
    public class GameEventView : MonoBehaviour, IGameEventView
    {
        private bool _pressed;
        private IGameDataEvent _eventData;
        public IGameDataEvent EventData => _eventData;

        //remove these after setting up the constructor
        [SerializeField] private GameEventSettings _settings;

        private Vector2 _curTouchScreenPos;
        private Vector2 _neutralPos;

        private IInputManager _inputManager;
        private IBankManager _bankManager;
        private IEventViewAccess _eventViewAccess;
        private Action<bool, IGameEventView> _resolutionCb;
        private Camera _camera;

        public void Init(IInputManager inputManager, IEventViewAccess eventManager, IBankManager bankManager,
            Action<bool, IGameEventView> resolutionCb, Camera camera)
        {
            _camera = camera;
            _inputManager = inputManager;
            _eventViewAccess = eventManager;
            _bankManager = bankManager;
            _resolutionCb = resolutionCb;
            _eventData = new LoanGameEventData(1, "event text", "title",
                resolutionCb, 100, 75, GameEventType.Loan, null);
        }

        public void ActivateEvent()
        {
            _eventViewAccess.EventValidation((LoanGameEventData)_eventData);
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
            _curTouchScreenPos = _inputManager.RecentTouch.ScreenPosition;

            //---------   TODO:  MAKE A UNIT TEST FOR THIS -------//
            transform.position = (Vector2)_camera.ScreenToWorldPoint(_curTouchScreenPos);
            if (Mathf.Abs(transform.position.x) - _neutralPos.x > _settings.DragDistanceToResolveCard)
            {
                if (transform.position.x > _neutralPos.x)
                    OnYesResult();
                else
                    OnNoResult();
            }
            //--------                               --------//
        }

        private void OnMouseDown()
        {
            //this event was pressed
            _pressed = true;
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
            if (res == false)
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