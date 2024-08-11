using UnityEngine;

namespace Features.Grid
{
    public static class TileComponentExtensions
    {
        public static TileColorComponent GetColorComponent(this Tile tile)
        {
            return tile.GetComponent<TileColorComponent>();
        }
        
        public static TileTextComponent GetTextComponent(this Tile tile)
        {
            return tile.GetComponent<TileTextComponent>();
        }
        
        public static TileOccupyComponent GetOccupyComponent(this Tile tile)
        {
            return tile.GetComponent<TileOccupyComponent>();
        }
        
        public static TilePositionComponent GetPositionComponent(this Tile tile)
        {
            return tile.GetComponent<TilePositionComponent>();
        }
        
        public static bool IsOccupied(this Tile tile)
        {
            return tile.GetOccupyComponent().Occupant != null;
        }

        public static void SetText(this Tile tile, string text)
        {
            tile.GetTextComponent();
        }
        
        public static void SetColor(this Tile tile, Color clr)
        {
            tile.GetColorComponent();
        }
        
        public static TileComponent AddComponent(this Tile tile, string componentName)
        {
            TileComponent component = null;
            switch (componentName)
            {
                case "TileColorComponent":
                    component = tile.gameObject.AddComponent<TileColorComponent>();
                    break;
                case "TileTextComponent":
                    component = tile.gameObject.AddComponent<TileTextComponent>();
                    break;
                case "TileOccupyComponent":
                    component = tile.gameObject.AddComponent<TileOccupyComponent>();
                    break;
            }

            component.Register(tile);
            return component;
        }
    }
}