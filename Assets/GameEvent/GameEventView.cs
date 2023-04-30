using System;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;

namespace Assets.GameEvent
{
    public class GameEventView : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private Action _yesResultAction, _noResultAction;
        private bool _pressed;
        private Camera _camera;
        private PointerEventData _touchData;

        [Inject]
        public void Construct(Action yes, Action no, Camera cam)
        {
            _yesResultAction = yes;
            _noResultAction = no;
            _camera = cam;
        }

        private void Update()
        {
            if (!_pressed)
                return;

            //var touchPos =  _camera.ScreenToWorldPoint(_touchData.worldPosition );
            //transform.position = touchPos;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _pressed = false;
            _touchData = null;
            //check for result or snap back into place
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _pressed = true;
            _touchData = eventData;
        }

        private void OnYesResult()
        {
            _yesResultAction();
        }

        private void OnNoResult()
        {
            _noResultAction();
        }

        
    }
}
