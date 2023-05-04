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
        private readonly IUiManager _uiManager;
        private readonly IAssetRefs _assetRefs;
        
        public GameDirector(EventBus bus, IAssetRefs assetRefs)
        {
            //var assetRefs = Resources.Load("AssetRefs");
            _assetRefs = assetRefs;
            
            _eventBus = bus;
            _uiManager = new UiManager(assetRefs);
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