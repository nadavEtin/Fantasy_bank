using System;
using UnityEngine;

namespace Assets.GameCore.Utility.ObjectPool
{
    public interface IPoolable
    {
        Action<GameObject> ObjectPoolCb { get; }
        void SetupReturnToPoolCb(Action<GameObject> cb);
        void ExecutePoolCb();
    }
}