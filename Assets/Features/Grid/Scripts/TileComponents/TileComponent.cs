using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace Features.Grid
{
    public abstract class TileComponent : MonoBehaviour
    {
        public static readonly List<string> Names = new List<string>
        {
            "TileColorComponent",
            "TileTextComponent",
            "TileOccupyComponent",
            "TilePositionComponent"
        };
        
        public Tile Tile;
        [Dropdown("Names")]
        public string ComponentName;
        
        public void Register(Tile tile)
        {
            this.Tile = tile;
            tile.RegisterComponent(this);
        }

        private void Reset()
        {
            if (Tile == null)
            {
                Tile = GetComponentInParent<Tile>();
            }
        }
    }
}