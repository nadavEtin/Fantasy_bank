using UnityEngine;

[CreateAssetMenu(fileName = "GameEventSettings", menuName = "Scriptable objects/Game event settings")]
public class GameEventSettings : ScriptableObject
{
    public float DragDistanceToResolveCard;
    public Vector2 CardNeutralPos;
}
