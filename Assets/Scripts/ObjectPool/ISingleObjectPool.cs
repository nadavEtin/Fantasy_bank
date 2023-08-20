using UnityEngine;

namespace Assets.Scripts.ObjectPool
{
    public interface ISingleObjectPool
    {
        void AddObjectToPool(GameObject obj);
        GameObject GetObjectFromPool();
    }
}