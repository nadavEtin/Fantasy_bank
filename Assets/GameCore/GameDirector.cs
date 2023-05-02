using Assets.Scripts.Utility;
using GameCore.ScriptableObjects;
using GameCore.UI;
using UnityEngine;
using VContainer.Unity;

namespace GameCore
{
    public class GameDirector : IStartable, ITickable
    {
        private readonly EventBus _eventBus;
        private readonly IUIManager _uiManager;
        private readonly IAssetRefs _assetRefs;
        
        public GameDirector(EventBus bus)
        {
            var assetRefs = Resources.Load("AssetRefs") as GameObject;
            _assetRefs = assetRefs.GetComponent<AssetRefs>();
            
            _eventBus = bus;
            _uiManager = new UiManager(_assetRefs);
        }

        private void LoadResources()
        {
            
        }
        
        public void Start()
        {
            
        
        }

        public void Tick()
        {
            
        }
    }
}