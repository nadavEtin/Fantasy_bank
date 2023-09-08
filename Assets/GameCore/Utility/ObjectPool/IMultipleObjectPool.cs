using UnityEngine;

namespace Assets.GameCore.Utility.ObjectPool
{
    public interface IMultipleObjectPool
    {
        void AddObjectToPool(GameObject obj, ObjectTypes type);

        GameObject GetObjectFromPool(ObjectTypes type);
    }
}
