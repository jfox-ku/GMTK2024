using Features.Grid;
using UnityEngine;

namespace Features.Building.Scripts.Components
{
    [RequireComponent(typeof(TileOccupant))]
    public class BuildingPlacementComponent : BuildingComponent
    {
        public bool IsPlaced;
        public TileOccupant TileOccupant;
    }
}