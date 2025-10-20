using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class TestingScript : MonoBehaviour
{
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

    void CalculateMaxX()
    {
        float screenHalfWidth = _mainCamera.orthographicSize * _mainCamera.aspect;
        float cardHalfWidth = _renderer.bounds.size.x / 2f;
        float visibleCardWidth = cardHalfWidth * (1f - offscreenPercent);
        _maxX = screenHalfWidth - visibleCardWidth;
    }

    void OnMouseDown()
    {
        _isDragging = true;
        Vector3 mouseWorld = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        _mouseOffsetX = transform.position.x - mouseWorld.x;
    }

    void OnMouseUp()
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

    void Update()
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

    void OnDrawGizmosSelected()
    {
        if (_mainCamera == null || _renderer == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(_startPos + Vector3.right * _maxX, _startPos + Vector3.right * _maxX + Vector3.up * 0.2f);
        Gizmos.DrawLine(_startPos - Vector3.right * _maxX, _startPos - Vector3.right * _maxX + Vector3.up * 0.2f);
    }
}
