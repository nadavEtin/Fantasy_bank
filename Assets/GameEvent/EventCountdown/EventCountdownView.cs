using System;
using Assets.GameCore.Utility.ObjectPool;
using DG.Tweening;
using GameCore.Utility.Screen;
using TMPro;
using UnityEngine;
using VContainer;

namespace GameEvent.EventCountdown
{
    [RequireComponent(typeof(RectTransform))]
    public class EventCountdownView : MonoBehaviour, IEventCountdownView, IPoolable
    {
        [SerializeField] private TextMeshProUGUI _eventName, _countdownNum;
        
        public int CountdownDuration { get; private set; }
        public RectTransform ObjTransform { get; private set; }
        //public int Id => _eventData.ID;
        public IGameDataEvent EventData { get; private set; }

        public Action<GameObject> ObjectPoolCb { get; private set; }

        private ScreenParams _screenParams;
        private Vector2 _defaultHiddenPos;
        private bool _extended;
        private RectTransform _rectTransform;
        //private Action<IGameDataEvent> _resolutionCb;
        

        public void Setup(IGameDataEvent eventData)
        {
            EventData = eventData;
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
            ExecutePoolCb();
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

        public void SetupReturnToPoolCb(Action<GameObject> cb)
        {
            ObjectPoolCb = cb;
        }

        public void ExecutePoolCb()
        {
            ObjectPoolCb(gameObject);
        }
    }
}
