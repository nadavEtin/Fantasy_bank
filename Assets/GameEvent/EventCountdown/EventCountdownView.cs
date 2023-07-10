using System;
using DG.Tweening;
using GameCore.Utility.Screen;
using TMPro;
using UnityEngine;
using VContainer;

namespace GameEvent.EventCountdown
{
    [RequireComponent(typeof(RectTransform))]
    public class EventCountdownView : MonoBehaviour, IEventCountdownView
    {
        [SerializeField] private TextMeshProUGUI _eventName, _countdownNum;
        
        public int CountdownDuration { get; private set; }
        public RectTransform ObjTransform { get; private set; }
        public int Id => _eventData.ID;

        private ScreenParams _screenParams;
        private Vector2 _defaultHiddenPos;
        private bool _extended;
        private RectTransform _rectTransform;
        //private Action<IGameDataEvent> _resolutionCb;
        private IGameDataEvent _eventData;

        public void Setup(IGameDataEvent eventData)
        {
            _eventData = eventData;
            ObjTransform = GetComponent<RectTransform>();
            CountdownDuration = eventData.CountdownDuration;
            _eventName.text = eventData.EventTitle;
            _countdownNum.text = CountdownDuration.ToString();
            //_resolutionCb = cb;
        }
        
        public void OnClick()
        {
            if(_extended)
                PullbackView();
            else
                ExtendView();
        }

        public void ReduceCountdown(int amount)
        {
            CountdownDuration -= amount;
            /*if(CountdownDuration <= 0)
                _resolutionCb.Invoke(_eventData);*/
        }

        public void CountdownDone()
        {
            //disappear the countdown view, with animation?
            gameObject.SetActive(false);
            //send the view obj to its object pool
            
        }

        [Inject]
        private void Construct(ScreenParams screenParams)
        {
            _screenParams = screenParams;
            //_defaultHiddenPos = new Vector2(_screenParams.RightEdgeXPos + 0.5f, transform.position.y);
            //transform.position = _defaultHiddenPos;
            _defaultHiddenPos = transform.position;
            _rectTransform = GetComponent<RectTransform>();
        }

        private void ExtendView()
        {
            _extended = true;
            _rectTransform.DOMoveX(_rectTransform.localPosition.x - 300, 0.5f);
            //transform.DOMoveX(_defaultHiddenPos.x - 1f, 0.5f);
        }

        private void PullbackView()
        {
            _extended = false;
            _rectTransform.DOMoveX(_defaultHiddenPos.x, 0.5f);
            //transform.DOMoveX(_defaultHiddenPos.x, 0.5f);
        }
    }
}
