using UnityEngine;

namespace GameCore.Utility.GeneralClasses
{
    public interface IBaseFactory
    {
        GameObject Create();
        GameObject Create(Transform parent);
    }
}