using NaughtyAttributes;
using UnityEngine;

namespace Features.Building.Scripts
{
    [CreateAssetMenu(menuName = "Building/BuildingAsset")]
    public class BuildingAsset : ScriptableObject
    {
        public string Name;
        public Sprite Icon;
        [ShowAssetPreview()]
        public BuildingBase Prefab;
        
        public BuildingBase CreateInScene()
        {
            return Instantiate(Prefab);
        }
    }
}