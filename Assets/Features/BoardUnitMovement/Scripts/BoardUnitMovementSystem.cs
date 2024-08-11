using DefaultNamespace;
using Features.Grid;
using NaughtyAttributes;
using UnityEngine;

namespace Features.BoardUnitMovement
{
    [CreateAssetMenu(menuName = "BoardUnitMovement/BoardUnitMovementSystem")]
    public class BoardUnitMovementSystem: ScriptableObject, IInit, ICleanUp, IExecute
    {
        TileCaster tileCaster;

        public Tile LastSelectedTile;
        
        public void Init()
        {
            tileCaster = new TileCaster();
            LastSelectedTile = null;
        }

        public void CleanUp()
        {
            
        }

        public void Execute()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var tile = tileCaster.Cast();
                LastSelectedTile = tile;
            }
            
            
        }
    }
}