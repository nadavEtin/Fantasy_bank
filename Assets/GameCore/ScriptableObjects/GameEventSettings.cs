using UnityEngine;

namespace GameCore.ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameEventSettings", menuName = "Scriptable objects/Game event settings")]
    public class GameEventSettings : ScriptableObject, IGameEventSettings
    {
        [SerializeField] private float _dragDistanceToResolveCard;
        [SerializeField] private Vector2 _cardNeutralPos;
        
        [Tooltip("Percent of screen size. X is width Y is height")]
        [SerializeField] private Vector2 _eventResolutionViewSize;
        [SerializeField] private Vector2 _eventResolutionPos;
        
        public int DefaultEventCountReduction { get; set; }
        public float DragDistanceToResolveCard => _dragDistanceToResolveCard;
        public Vector2 CardNeutralPos => _cardNeutralPos;
        public Vector2 EventResolutionViewSize => _eventResolutionViewSize;
        public Vector2 EventResolutionNeutralPos => _eventResolutionPos;
    }
}
