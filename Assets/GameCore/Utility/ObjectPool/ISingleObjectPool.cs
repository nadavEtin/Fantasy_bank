using UnityEngine;

namespace Assets.GameCore.Utility.ObjectPool
{
    public interface ISingleObjectPool
    {
        void AddObjectToPool(GameObject obj);
        GameObject GetObjectFromPool();
    }
}