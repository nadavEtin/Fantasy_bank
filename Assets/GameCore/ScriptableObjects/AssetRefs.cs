using UnityEngine;

namespace GameCore.ScriptableObjects
{
    [CreateAssetMenu(fileName = "AssetRefs", menuName = "Scriptable objects/Asset References")]
    public class AssetRefs : ScriptableObject, IAssetRefs
    {
        //Prefabs
        [SerializeField] private GameObject _goldDisplay;
        
        //game events
        [SerializeField] private GameObject _eventCountdown, _gameEvent, _eventResolutionScreen;

        public GameObject GoldDisplay => _goldDisplay;
        public GameObject GameEvent => _gameEvent;
        public GameObject EventCountdown => _eventCountdown;
        public GameObject EventResolutionScreen => _eventResolutionScreen;
    }
}