using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Features.Grid
{
    public class Tile : MonoBehaviour
    {
        public Dictionary<string, TileComponent> TileComponents;

        public TextMeshPro Text;
        public MeshRenderer MeshRenderer;

        public void RegisterChildren()
        {
            var components = GetComponentsInChildren<TileComponent>();
            foreach (var component in components)
            {
                component.Register(this);
            }
        }
        
        public void RegisterComponent(TileComponent tileComponent)
        {
            TileComponents ??= new Dictionary<string, TileComponent>();

            if (TileComponents.ContainsKey(tileComponent.ComponentName))
            {
                Debug.LogError("Component already registered");
                return;
            }
            
            TileComponents.Add(tileComponent.ComponentName, tileComponent);
        }
        
        public void RemoveComponent(TileComponent tileComponent)
        {
            if (TileComponents == null) return;
            if (TileComponents.ContainsKey(tileComponent.ComponentName))
            {
                TileComponents.Remove(tileComponent.ComponentName);
            }
        }
        
        public void RemoveComponent(string componentName)
        {
            if (TileComponents == null) return;
            if (TileComponents.ContainsKey(componentName))
            {
                TileComponents.Remove(componentName);
            }
        }
        
        public void GetTileComponent<T>(string componentName, out T component) where T : TileComponent
        {
            component = null;
            if (TileComponents == null) return;
            if (TileComponents.ContainsKey(componentName))
            {
                component = TileComponents[componentName] as T;
            }
        }

        public bool TryGetTileComponent<T>(string componentName, out T component) where T : TileComponent
        {
            GetTileComponent<T>(componentName, out component);
            return component != null;
        }
        
    }
}