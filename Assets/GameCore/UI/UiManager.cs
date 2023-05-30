using GameCore.ScriptableObjects;
using TMPro;
using UnityEngine;

namespace GameCore.UI
{
    public class UiManager : IUiManager
    {
        private readonly IAssetRefs _assetRefs;
        private Canvas _canvas;

        private TextMeshProUGUI _goldDisplayText;
        
        public UiManager(IAssetRefs assetRefs, Canvas canvas)
        {
            _assetRefs = assetRefs;
            _canvas = canvas;
            var goldText = GameObject.Instantiate(_assetRefs.GoldDisplay, _canvas.transform);
            goldText.name = "GoldText";
            _goldDisplayText = goldText.GetComponentInChildren<TextMeshProUGUI>();
        }

        public void GoldBalanceUpdate(int curAmnt)
        {
            _goldDisplayText.text = string.Format("Gold: {0}", curAmnt);
        }
    }
}