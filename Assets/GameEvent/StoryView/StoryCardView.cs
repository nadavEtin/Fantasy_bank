using Assets.GameCore.Utility.ObjectPool;
using Bank;
using GameCore.EventBus;
using GameCore.EventBus.GameplayEvents;
using GameCore.Input;
using GameCore.ScriptableObjects;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using VContainer;

namespace GameEvent.StoryView
{
    public class StoryCardView : MonoBehaviour, IStoryCardView, IPoolable
    {
        public IGameDataEvent EventData => _storyData;
        public Action<GameObject> ObjectPoolCb { get; set; }
        [SerializeField] private SpriteRenderer _background, _cardImg;
        [SerializeField] private TextMeshPro _title, _mainText;

        //private IGameEventSettings _settings;
        //private bool _pressed;
        private IGameDataEvent _storyData;
        //private Vector2 _curTouchScreenPos;
        //private Vector2 _neutralPos;
        //private IInputManager _inputManager;
        //private IBankBalance _bankBalance;
        private Action<bool, IGameDataEvent> _resolutionCb;
        //private Camera _camera;
        private EventsManager _eventBus;

        //private bool isDragging = false;
        //private Vector3 offset;
        //private Vector3 startPosition;
        //private Quaternion startRotation;

        [SerializeField] private float maxRotationAngle = 45f; // Maximum rotation angle in degrees



        [Header("Settings")]
        [Range(0f, 0.5f)]
        public float offscreenPercent = 0.15f; // How much of the card can go out of view
        public float returnSpeed = 5f;         // How fast it returns to center
        public float dragSmoothness = 10f;     // How smooth dragging feels
        public float maxTiltAngle = 15f;       // How much it tilts at max drag
        [Range(0f, 1f)]
        public float swipeTriggerThreshold = 0.9f; // Percent of max distance to count as swipe

        [Header("Color Feedback")]
        public Color leftColor = Color.red;
        public Color rightColor = Color.green;
        [Range(0f, 1f)]
        public float colorStartThreshold = 0.3f; // How far you must drag before color starts changing
        public float colorIntensity = 1f;        // Strength of the color change

        [Header("Events")]
        public UnityEvent onSwipeLeft;
        public UnityEvent onSwipeRight;

        private Vector3 _startPos;
        private bool _isDragging = false;
        private float _mouseOffsetX;
        private float _maxX;
        private Camera _mainCamera;
        private Renderer _renderer;
        private Color _originalColor;

        void Start()
        {
            _mainCamera = Camera.main;
            _renderer = GetComponent<Renderer>();

            // Clone material to avoid changing shared material
            _renderer.material = new Material(_renderer.material);
            _originalColor = _renderer.material.color;

            _startPos = transform.position;
            CalculateMaxX();
        }

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
                //OnNoResult();
            }
        }

        private void CalculateMaxX()
        {
            var screenHalfWidth = _mainCamera.orthographicSize * _mainCamera.aspect;
            var cardHalfWidth = _renderer.bounds.size.x / 2f;
            var visibleCardWidth = cardHalfWidth * (1f - offscreenPercent);
            _maxX = screenHalfWidth - visibleCardWidth;
        }

        private void OnMouseDown()
        {
            _isDragging = true;
            Vector3 mouseWorld = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            _mouseOffsetX = transform.position.x - mouseWorld.x;
        }

        private void OnMouseUp()
        {
            float distanceFromCenter = transform.position.x - _startPos.x;
            float normalized = Mathf.Abs(distanceFromCenter) / _maxX;

            if (normalized >= swipeTriggerThreshold)
            {
                if (distanceFromCenter > 0)
                    onSwipeRight?.Invoke();
                else
                    onSwipeLeft?.Invoke();
            }

            _isDragging = false;
        }

        private void Update()
        {
            if (_isDragging)
            {
                Vector3 mouseWorld = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
                float targetX = mouseWorld.x + _mouseOffsetX;
                targetX = Mathf.Clamp(targetX, _startPos.x - _maxX, _startPos.x + _maxX);

                Vector3 targetPos = new Vector3(targetX, _startPos.y, _startPos.z);
                transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * dragSmoothness);
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, _startPos, Time.deltaTime * returnSpeed);
            }

            // --- Tilt ---
            float normalizedX = (transform.position.x - _startPos.x) / _maxX;
            float targetTilt = -normalizedX * maxTiltAngle;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, targetTilt), Time.deltaTime * dragSmoothness);

            // --- Color Feedback ---
            float absNormalized = Mathf.Abs(normalizedX);

            if (absNormalized > colorStartThreshold)
            {
                float t = Mathf.InverseLerp(colorStartThreshold, 1f, absNormalized);
                Color targetColor = normalizedX > 0 ? rightColor : leftColor;
                Color blended = Color.Lerp(_originalColor, targetColor, t * colorIntensity);
                _renderer.material.color = blended;
            }
            else
            {
                _renderer.material.color = Color.Lerp(_renderer.material.color, _originalColor, Time.deltaTime * returnSpeed);
            }
        }

        //TODO: remove this
        private void OnDrawGizmosSelected()
        {
            if (_mainCamera == null || _renderer == null) return;
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(_startPos + Vector3.right * _maxX, _startPos + Vector3.right * _maxX + Vector3.up * 0.2f);
            Gizmos.DrawLine(_startPos - Vector3.right * _maxX, _startPos - Vector3.right * _maxX + Vector3.up * 0.2f);
        }

        public void OnYesResult()
        {
            Debug.Log("yes result");
            //_pressed = false;

            //TODO: invoke yesCb that should check if theres enoguh balance in the bank
            /*var res = _bankBalance.GetGoldFromBank(_eventData.LoanPrice);
            if (res == false)
                OnNoResult();*/

            //_gameEventManager.
            //TODO: continue process after approved loan
            _eventBus.Publish(GameplayEvent.StoryEventApproved, new EventApprovedParams(EventData));
            //_eventData.ResolutionCb(true, this);

            //temp
            //SnapToNeutralPos();
        }

        public void OnNoResult()
        {
            Debug.Log("no result");
            //_pressed = false;
            //_eventData.ResolutionCb(false, this);

            //temp
            //SnapToNeutralPos();
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


        [Inject]
        private void Setup(IInputManager inputManager, IBankBalance bankBalance, IGameEventSettings settings,
            Camera camera, EventsManager eventBus)
        {
            //_camera = camera;
            //_inputManager = inputManager;
            //_bankBalance = bankBalance;
            _eventBus = eventBus;
            //_settings = settings;
        }
    }
}