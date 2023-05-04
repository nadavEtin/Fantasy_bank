using GameCore.ScriptableObjects;
using TMPro;
using UnityEngine;

namespace GameCore.UI
{
    public class UiManager : IUiManager
    {
        private readonly IAssetRefs _assetRefs;

        private TextMeshProUGUI _goldDisplayText;

        public UiManager(IAssetRefs assetRefs)
        {
            _assetRefs = assetRefs;
            _goldDisplayText = GameObject.Instantiate(_assetRefs.GoldDisplay).GetComponentInChildren<TextMeshProUGUI>();
        }

        public void GoldBalanceUpdate(int curAmnt)
        {
            _goldDisplayText.text = string.Format("Gold: {0}", curAmnt);
        }
    }
}