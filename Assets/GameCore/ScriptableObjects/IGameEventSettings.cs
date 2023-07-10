using UnityEngine;

namespace GameCore.ScriptableObjects
{
    public interface IGameEventSettings
    {
        float DragDistanceToResolveCard { get; }
        Vector2 CardNeutralPos { get; }
        int DefaultEventCountReduction { get; set; }
    }
}