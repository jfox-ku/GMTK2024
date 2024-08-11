using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace Features.Building.Scripts
{
    public abstract class BuildingComponent : MonoBehaviour
    {
        public static readonly List<string> Names = new List<string>
        {
            "HealthComponent",
            "SpawnerComponent",
            "MeshComponent",
            "PlacementComponent",
            "SpriteComponent",
        };
        
        public BuildingBase Building;
        [Dropdown("Names")]
        public string ComponentName;
        
        public void Register(BuildingBase building)
        {
            this.Building = building;
            building.RegisterComponent(this);
        }
    }
}