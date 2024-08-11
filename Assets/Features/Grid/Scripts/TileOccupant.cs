using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace Features.Grid
{
    public class TileOccupant : MonoBehaviour
    {
        public static List<int> Rotations = new List<int> {0, 1, 2, 3};

        [Dropdown("Rotations")]
        public int Rotation; // 0 = 0 degrees, 1 = 90 degrees, 2 = 180 degrees, 3 = 270 degrees
        public List<Vector2Int> LocalPositions;
        private List<Tile> OccupiedTiles;

        public void OccupyTile(Tile tile)
        {
            tile.GetOccupyComponent().Occupant = this;
            OccupiedTiles ??= new List<Tile>();
            OccupiedTiles.Add(tile);
        }
        
        public void Lift()
        {
            foreach (var tile in OccupiedTiles)
            {
                tile.GetOccupyComponent().Occupant = null;
            }
            
            OccupiedTiles.Clear();
        }
        
        public IEnumerable<Vector2Int> WithLocalOffsets(Vector2Int gridPos)
        {
            foreach (var localPosition in LocalPositions)
            {
                yield return RotatePosition(localPosition) + gridPos;
            }
        }

        private Vector2Int RotatePosition(Vector2Int position)
        {
            switch (Rotation % 4) // Ensure Rotation is within 0-3
            {
                case 1: // 90 degrees
                    return new Vector2Int(-position.y, position.x);
                case 2: // 180 degrees
                    return new Vector2Int(-position.x, -position.y);
                case 3: // 270 degrees
                    return new Vector2Int(position.y, -position.x);
                default: // 0 degrees (no rotation)
                    return position;
            }
        }

        public void OnDrawGizmos()
        {
            if (LocalPositions == null) return;
            Gizmos.color = Color.yellow;
            foreach (var localPosition in LocalPositions)
            {
                var rotatedPosition = RotatePosition(localPosition);
                Gizmos.DrawWireCube(transform.position + new Vector3(rotatedPosition.x, 0, rotatedPosition.y), Vector3.one);
            }
        }
    }
}