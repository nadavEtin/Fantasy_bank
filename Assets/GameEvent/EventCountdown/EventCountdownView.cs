using System;
using Assets.GameCore.Utility.ObjectPool;
using DG.Tweening;
using GameCore.Utility.Screen;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace GameEvent.EventCountdown
{
    [RequireComponent(typeof(RectTransform))]
    public class EventCountdownView : MonoBehaviour, IEventCountdownView, IPoolable
    {
        [SerializeField] private TextMeshProUGUI _eventName, _countdownNum;
        [SerializeField, Range(0f, 1f)] private float _visibleWidthFraction = 0.3f;
        [SerializeField, Range(0f, 1f)] private float _extendedVisibleWidthFraction = 0.95f;

        public int CountdownDuration { get; private set; }
        public RectTransform ObjTransform { get; private set; }
        //public int Id => _eventData.ID;
        public IGameDataEvent EventData { get; private set; }

        public Action<GameObject> ObjectPoolCb { get; private set; }

        private ScreenParams _screenParams;
        private Vector2 _defaultHiddenAnchoredPos;
        private Vector2 _extendedAnchoredPos;
        private bool _extended;
        private RectTransform _rectTransform;
        //private Action<IGameDataEvent> _resolutionCb;
        

        public void Setup(IGameDataEvent eventData)
        {
            EventData = eventData;
            ObjTransform = GetComponent<RectTransform>();
            _rectTransform = ObjTransform;
            CountdownDuration = eventData.CountdownDuration;
            _eventName.text = eventData.EventTitle;
            _countdownNum.text = CountdownDuration.ToString();
            _extended = false;
            _rectTransform.DOKill();
            ApplyPeekPosition();
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
            _rectTransform = GetComponent<RectTransform>();
        }

        private void ApplyPeekPosition()
        {
            EnsureIgnoreLayout();
            Canvas.ForceUpdateCanvases();

            var canvasRect = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
            var width = _rectTransform.rect.width;
            var height = _rectTransform.rect.height;
            if (width <= 0f)
                width = canvasRect.rect.width * 0.5985f;
            if (height <= 0f)
                height = 80f;

            _rectTransform.anchorMin = new Vector2(1f, 0.5f);
            _rectTransform.anchorMax = new Vector2(1f, 0.5f);
            _rectTransform.pivot = new Vector2(1f, 0.5f);
            _rectTransform.sizeDelta = new Vector2(width, height);

            // Right-anchored: positive X pushes off-screen to the right.
            var y = _rectTransform.anchoredPosition.y;
            _defaultHiddenAnchoredPos = new Vector2(width * (1f - _visibleWidthFraction), y);
            _extendedAnchoredPos = new Vector2(width * (1f - _extendedVisibleWidthFraction), y);
            _rectTransform.anchoredPosition = _defaultHiddenAnchoredPos;
        }

        private void EnsureIgnoreLayout()
        {
            if (!TryGetComponent(out LayoutElement layoutElement))
                layoutElement = gameObject.AddComponent<LayoutElement>();
            layoutElement.ignoreLayout = true;
        }

        public void SetStackIndex(int index, float spacing)
        {
            _rectTransform.anchoredPosition = new Vector2(_defaultHiddenAnchoredPos.x, spacing * index);
            if (!_extended)
                _defaultHiddenAnchoredPos.y = _rectTransform.anchoredPosition.y;
            _extendedAnchoredPos.y = _rectTransform.anchoredPosition.y;
        }

        private void ExtendView()
        {
            _extended = true;
            _rectTransform.DOAnchorPos(_extendedAnchoredPos, 0.5f);
        }

        private void PullbackView()
        {
            _extended = false;
            _rectTransform.DOAnchorPos(_defaultHiddenAnchoredPos, 0.5f);
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
