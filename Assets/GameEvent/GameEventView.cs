using System;
using Lean.Touch;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;

namespace Assets.GameEvent
{
    public class GameEventView : MonoBehaviour
    {
        private Action _yesResultAction, _noResultAction;
        private bool _pressed;
        
        //remove these after setting up the constructor
        [SerializeField] private Camera _camera;
        [SerializeField] private GameEventSettings _settings;

        private LeanFinger _curFinger;

        private Vector2 _neutralPos;
        //private PointerEventData _touchData;

        [Inject]
        public void Construct(Action yes, Action no, Camera cam)
        {
            _yesResultAction = yes;
            _noResultAction = no;
            _camera = cam;
            _neutralPos = _settings.CardNeutralPos;
        }

        private void OnEnable()
        {
            LeanTouch.OnFingerDown += TouchStarted;
            LeanTouch.OnFingerUp += TouchEnded;
        }

        private void OnDisable()
        {
            LeanTouch.OnFingerDown -= TouchStarted;
            LeanTouch.OnFingerUp -= TouchEnded;
        }

        private void Update()
        {
            if (!_pressed)
                return;
            
            //---------   TODO:  MAKE A UNIT TEST FOR THIS -------//
            transform.position = (Vector2)_camera.ScreenToWorldPoint(_curFinger.ScreenPosition);
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
            //_curFinger = LeanTouch.Fingers[0];
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

        private void TouchStarted(LeanFinger finger)
        {
            _curFinger = LeanTouch.Fingers[0];
        }

        private void TouchEnded(LeanFinger finger)
        {
            _curFinger = null;
        }

        /*public void OnPointerUp(PointerEventData eventData)
        {
            _pressed = false;
            _touchData = null;
            //check for result or snap back into place
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _pressed = true;
            _touchData = eventData;
        }*/

        private void OnYesResult()
        {
            //_yesResultAction();
            Debug.Log("yes result");
            _pressed = false;
            
            //temp
            SnapToNeutralPos();
        }

        private void OnNoResult()
        {
            //_noResultAction();
            Debug.Log("no result");
            _pressed = false;
            
            //temp
            SnapToNeutralPos();
        }
    }
}
