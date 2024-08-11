using Features.Building.Scripts.Components;

namespace Features.Building.Scripts
{
    public static class BuildingBaseExtensions
    {
        public static BuildingPlacementComponent GetPlacementComponent(this BuildingBase building)
        {
            return building.GetComponent<BuildingPlacementComponent>();
        }
        
        public static BuildingComponent GetComponent(this BuildingBase building, string componentName)
        {
            return building.BuildingComponents[componentName];
        }
    }
}