using System.Collections.Generic;
using UnityEngine;

namespace Features.Building.Scripts
{
    public class BuildingBase : MonoBehaviour
    {
        public Dictionary<string, BuildingComponent> BuildingComponents;
        
        public void RegisterChildren()
        {
            var components = GetComponentsInChildren<BuildingComponent>();
            foreach (var component in components)
            {
                component.Register(this);
            }
        }

        public void RegisterComponent(BuildingComponent buildingComponent)
        {
            BuildingComponents ??= new Dictionary<string, BuildingComponent>();
            
            if (BuildingComponents.ContainsKey(buildingComponent.ComponentName))
            {
                Debug.LogError("Component already registered");
                return;
            }

            BuildingComponents.Add(buildingComponent.ComponentName , buildingComponent);
        }
    }
}