using Assets.GameCore.Utility.ObjectPool;
using Bank;
using GameCore.EventBus;
using GameCore.EventBus.GameplayEvents;
using GameCore.Input;
using GameCore.ScriptableObjects;
using System;
using TMPro;
using UnityEngine;
using VContainer;

namespace GameEvent.StoryView
{
    public class StoryCardView : MonoBehaviour, IStoryCardView, IPoolable
    {
        public IGameDataEvent EventData => _storyData;
        public Action<GameObject> ObjectPoolCb { get; set; }
        [SerializeField] private SpriteRenderer _background, _cardImg;
        [SerializeField] private TextMeshPro _title, _mainText;

        private IGameEventSettings _settings;
        private bool _pressed;
        private IGameDataEvent _storyData;
        private Vector2 _curTouchScreenPos;
        private Vector2 _neutralPos;
        private IInputManager _inputManager;
        private IBankBalance _bankBalance;
        private Action<bool, IGameDataEvent> _resolutionCb;
        private Camera _camera;
        private EventBus _eventBus;

        public void Init(IGameDataEvent storyData, Action<bool, IGameDataEvent> resolutionCb)
        {
            _storyData = storyData;
            _resolutionCb = resolutionCb;

            _title.text = storyData.EventTitle;
            _mainText.text = storyData.EventText;
        }

        public void ActivateEvent(bool shouldActivate)
        {
            //var res = _eventViewAccess.EventValidation((LoanGameEventData)_eventData);
            if (shouldActivate)
            {
                //TODO: show the event

            }
            else
            {

                //TODO: disallow swiping right, paint cost red?
                OnNoResult();
            }
        }

        [Inject]
        private void Setup(IInputManager inputManager, IBankBalance bankBalance, IGameEventSettings settings,
            Camera camera, EventBus eventBus)
        {
            _camera = camera;
            _inputManager = inputManager;
            _bankBalance = bankBalance;
            _eventBus = eventBus;
            _settings = settings;
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

            //launch object pool cb?
            ExecutePoolCb();
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

        public void SetupReturnToPoolCb(Action<GameObject> cb)
        {
            //Save the object pool return cb
            ObjectPoolCb = cb;
        }

        public void ExecutePoolCb()
        {
            ObjectPoolCb(gameObject);
        }
    }
}