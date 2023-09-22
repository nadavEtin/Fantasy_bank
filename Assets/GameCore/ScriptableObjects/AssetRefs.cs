using UnityEngine;

namespace GameCore.ScriptableObjects
{
    [CreateAssetMenu(fileName = "AssetRefs", menuName = "Scriptable Objects/Asset References")]
    public class AssetRefs : ScriptableObject, IAssetRefs
    {
        //Prefabs
        [SerializeField] private GameObject _goldDisplay;
        
        //game events
        [SerializeField] private GameObject _eventCountdown, _sotryView, _eventResolutionScreen;

        public GameObject GoldDisplay => _goldDisplay;
        public GameObject StoryView => _sotryView;
        public GameObject EventCountdown => _eventCountdown;
        public GameObject EventResolutionScreen => _eventResolutionScreen;
    }
}