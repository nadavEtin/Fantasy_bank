using UnityEngine;

namespace GameCore.ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameEventSettings", menuName = "Scriptable objects/Game event settings")]
    public class GameEventSettings : ScriptableObject, IGameEventSettings
    {
        [SerializeField] private float _dragDistanceToResolveCard;
        [SerializeField] private Vector2 _cardNeutralPos;
        
        public int DefaultEventCountReduction { get; set; }
        public float DragDistanceToResolveCard => _dragDistanceToResolveCard;
        public Vector2 CardNeutralPos => _cardNeutralPos;
    }
}
