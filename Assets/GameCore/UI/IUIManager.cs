using UnityEngine;

namespace GameCore.UI
{
    public interface IUiManager
    {
        void GoldBalanceUpdate(int curAmnt);
        ICanvasRefs CanvasRefs { get; }
        Canvas Canvas { get; }
    }
}