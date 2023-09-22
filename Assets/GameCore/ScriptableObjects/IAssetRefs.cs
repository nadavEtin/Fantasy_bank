using UnityEngine;

namespace GameCore.ScriptableObjects
{
    public interface IAssetRefs
    {
        public GameObject GoldDisplay { get; }
        public GameObject StoryView { get; }
        GameObject EventCountdown { get; }
        GameObject EventResolutionScreen { get; }
    }
}