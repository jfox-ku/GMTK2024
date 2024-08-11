using DefaultNamespace;
using Features.Building.Scripts.Components;
using Features.Grid;
using NaughtyAttributes;
using ScriptableObjectArchitecture;
using UnityEngine;

namespace Features.Building.Scripts
{
    [CreateAssetMenu(menuName = "BuildingMovementSystem")]
    public class BuildingMovementSystem : ScriptableObject, IInit, IExecute, ICleanUp
    {
        [ShowIf("HasSelectedBuilding")]
        public BuildingBase LastSelectedBuilding;
        public bool HasSelectedBuilding => LastSelectedBuilding != null;

        public TileCaster TileCaster;

        public GameStateVariable GameState;
        public GridSystem GridSystem;
        
        public void Init()
        {
            TileCaster = new TileCaster();
            if (GridSystem == null)
            {
                GridSystem = GameRoot.Instance.GetSystem<GridSystem>();
                if (GridSystem == null) Debug.LogError("GridSystem is null. MovementSystem will not work.");
            }
        }

        public void Execute()
        {
            if (GameState.Value is not GameStates.GamePlayingState) return;
            
            if(LastSelectedBuilding != null) HandleBuildingHoverMovement();
            
            if (!Input.GetMouseButtonDown(0)) return;
            
            var hit = TileCaster.Cast();
            if (hit == null) return;

            if (HasSelectedBuilding)
            {
                HandlePlaceBuilding(hit);
            }
            else
            {
                HandleSelectBuilding(hit);
            }
        }

        private void HandleBuildingHoverMovement()
        {
            var hit = TileCaster.Cast();
            if (hit == null) return;
            
            var placementComponent = LastSelectedBuilding.GetPlacementComponent();
            var positions = placementComponent.TileOccupant.WithLocalOffsets(hit.GetPositionComponent().Position);

            foreach (var gridPos in positions)
            {
                var gridTile = GridSystem.GetTile(gridPos);
                gridTile.SetColor(gridTile.GetOccupyComponent().IsEmpty ? Color.green : Color.red);
            }
        }

        private void HandleSelectBuilding(Tile hit)
        {
            var tileOccupant = hit.GetOccupyComponent().Occupant;
            if (tileOccupant != null)
            {
                LastSelectedBuilding = tileOccupant.GetComponent<BuildingBase>();
                if (LastSelectedBuilding == null) return;
                LiftBuilding(LastSelectedBuilding.GetPlacementComponent());
            }
        }

        private void HandlePlaceBuilding(Tile tile)
        {
            var placementComponent = LastSelectedBuilding.GetPlacementComponent();
            if (CanPlaceBuilding(placementComponent, tile))
            {
                PlaceBuilding(placementComponent,tile);
            }
        }

        private void LiftBuilding(BuildingPlacementComponent placementComponent)
        {
            placementComponent.TileOccupant.Lift();
            placementComponent.IsPlaced = false;
            
        }
        
        private void PlaceBuilding(BuildingPlacementComponent placementComponent, Tile tile)
        {
            var placementPositions = placementComponent.TileOccupant.WithLocalOffsets(tile.GetPositionComponent().Position);

            foreach (var pos in placementPositions)
            {
                var gridTile = GridSystem.GetTile(pos);
                gridTile.GetOccupyComponent().Occupant = placementComponent.TileOccupant;
                placementComponent.TileOccupant.OccupyTile(gridTile);
            }
            placementComponent.IsPlaced = true;
        }
        
        private bool CanPlaceBuilding(BuildingPlacementComponent placementComponent, Tile tile)
        {
            var checkPositions = placementComponent.TileOccupant.WithLocalOffsets(tile.GetPositionComponent().Position);

            foreach (var pos in checkPositions)
            {
                var gridTile = GridSystem.GetTile(pos);
                if (gridTile == null) return false;
                if (!gridTile.GetOccupyComponent().IsEmpty) return false;
            }

            return true;
        }

        public void CleanUp()
        {
            LastSelectedBuilding = null;
        }
    }
}