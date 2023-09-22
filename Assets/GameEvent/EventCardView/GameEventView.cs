using System;
using Bank;
using GameCore.EventBus;
using GameCore.EventBus.GameplayEvents;
using GameCore.Input;
using GameCore.ScriptableObjects;
using GameEvent.LoanEvent;
using UnityEngine;

namespace GameEvent.EventCardView
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
        private IBankBalance _bankBalance;
        private Action<bool, IGameEventView> _resolutionCb;
        private Camera _camera;
        private EventBus _eventBus;

        public void Init(IInputManager inputManager, IBankBalance bankBalance, Action<bool, IGameEventView> resolutionCb,
            Camera camera, EventBus eventBus)
        {
            _camera = camera;
            _inputManager = inputManager;
            _bankBalance = bankBalance;
            _eventBus = eventBus;
            _resolutionCb = resolutionCb;

            _eventData = new LoanGameEventData(1, "event text", "title", "resolution title", "resolution text", 5,
                resolutionCb, _bankBalance, 100, 75, StoryType.Loan, null);
        }

        /*public bool EventValidation()
        {
            return _eventData.RequirementsMetValidation();
        }*/

        public void ActivateEvent()
        {
            //var res = _eventViewAccess.EventValidation((LoanGameEventData)_eventData);
            /*if (res)
            {
                //TODO: show some 'not enough gold' message
                OnNoResult();
            }
            else
            {
                //TODO: show the event
            }*/
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
            
            //TODO: invoke yesCb that should check if theres enoguh balance in the bank
            /*var res = _bankBalance.GetGoldFromBank(_eventData.LoanPrice);
            if (res == false)
                OnNoResult();*/

            //_gameEventManager.
            //TODO: continue process after approved loan
            _eventBus.Publish(GameplayEvent.EventApproved, new EventApprovedParams(EventData));
            //_eventData.ResolutionCb(true, this);

            //temp
            SnapToNeutralPos();
        }

        private void OnNoResult()
        {
            Debug.Log("no result");
            _pressed = false;
            //_eventData.ResolutionCb(false, this);

            //temp
            SnapToNeutralPos();
        }
    }
}