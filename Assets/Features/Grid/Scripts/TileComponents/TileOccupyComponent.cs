namespace Features.Grid
{
    public class TileOccupyComponent : TileComponent
    {
        public TileOccupant Occupant;
        public bool IsEmpty => Occupant == null;
    }
}