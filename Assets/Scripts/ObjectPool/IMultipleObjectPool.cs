using UnityEngine;

namespace ObjectPool
{
    public interface IMultipleObjectPool
    {
        void AddObjectToPool(GameObject obj, ObjectTypes type);

        GameObject GetObjectFromPool(ObjectTypes type);
    }
}
