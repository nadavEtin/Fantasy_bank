using GameCore.ScriptableObjects;
using UnityEngine;
using VContainer;

namespace GameCore.Utility.GeneralClasses
{
    public abstract class BaseFactory : IBaseFactory
    {
        protected IAssetRefs _assetRefs;
        protected IObjectResolver _resolver;

        protected BaseFactory(IAssetRefs assetRefs, IObjectResolver resolver)
        {
            _assetRefs = assetRefs;
            _resolver = resolver;
        }

        public abstract GameObject Create();

        public abstract GameObject Create(Transform parent);
    }
}