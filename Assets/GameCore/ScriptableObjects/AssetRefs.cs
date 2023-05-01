using UnityEngine;

namespace GameCore.ScriptableObjects
{
    [CreateAssetMenu(fileName = "AssetRefs", menuName = "Scriptable objects/Asset References")]
    public class AssetRefs : ScriptableObject, IAssetRefs
    {
        //Prefabs
        [SerializeField] private GameObject _goldDisplay;

        public GameObject GoldDisplay => _goldDisplay;
    }
}