using GameCore.ScriptableObjects;

namespace GameCore.UI
{
    public class UiManager
    {
        private readonly IAssetRefs _assetRefs;

        public UiManager(IAssetRefs assetRefs)
        {
            _assetRefs = assetRefs;
        }
    }
}