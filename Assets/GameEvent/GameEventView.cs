using System;
using Assets.GameEvent.LoanEvent;
using GameCore;
using GameCore.Events;
using Lean.Touch;
using UnityEditor.DeviceSimulation;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;

namespace Assets.GameEvent
{
    public class GameEventView : MonoBehaviour
    {
        private bool _pressed;
        private LoanGameEventData _eventData;
        
        //remove these after setting up the constructor
        [SerializeField] private Camera _camera;
        [SerializeField] private GameEventSettings _settings;

        private TouchEventParams _curTouchData;
        private Vector2 _neutralPos;
        private IGameDirector _gameDirector;

        [Inject]
        public void Construct(IGameDirector gameDirector, Action yes, Action no, Camera cam)
        {
            _gameDirector = gameDirector;
            _eventData = new LoanGameEventData("yo", "man", yes, no, 100, 75);
            _camera = cam;
            _neutralPos = _settings.CardNeutralPos;
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
            _eventData.YesResult();
            
            //temp
            SnapToNeutralPos();
        }

        private void OnNoResult()
        {
            Debug.Log("no result");
            _pressed = false;
            _eventData.NoResult();
            
            //temp
            SnapToNeutralPos();
        }
    }
}
