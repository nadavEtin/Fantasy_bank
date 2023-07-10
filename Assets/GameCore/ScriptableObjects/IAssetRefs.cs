using UnityEngine;

namespace GameCore.ScriptableObjects
{
    public interface IAssetRefs
    {
        public GameObject GoldDisplay { get; }
        public GameObject GameEvent { get; }
        GameObject EventCountdown { get; }
    }
}