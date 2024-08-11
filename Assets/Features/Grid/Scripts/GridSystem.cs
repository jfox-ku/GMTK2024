using DefaultNamespace;
using NaughtyAttributes;
using UnityEngine;

namespace Features.Grid
{
    [CreateAssetMenu(menuName = "Grid/GridSystem")]
    public class GridSystem : ScriptableObject, IInit, ICleanUp
    {
        private global::Grid<Tile> _grid;

        public int width;
        public int depth;
        public float cellSize;

        public Transform GridParent => _gridParent ? _gridParent : _gridParent = new GameObject("GridParent").transform;
        public Tile TilePrefab;

        private Transform _gridParent;

        public void Init()
        {
            GenerateGridWithTiles();
        }

        public void CleanUp()
        {
            DestroyImmediate(_gridParent);
            _grid = null;
        }

        [Button()]
        public void DestroyGrid()
        {
            for (int i = GridParent.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(GridParent.GetChild(i).gameObject);
            }

            _grid = null;
        }

        [Button()]
        public void GenerateGridWithTiles()
        {
            DestroyGrid();
            _grid = new global::Grid<Tile>(width, depth, cellSize);
            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < depth; z++)
                {
                    Tile tile = Instantiate(TilePrefab, _grid.GetWorldPosition(x, z), Quaternion.identity);
                    tile.RegisterChildren();
                    var posText = "X: " + x + " Z: " + z;
                    tile.name = "Tile " + posText;
                    tile.SetText(posText);
                    tile.SetColor((x % 2 == 0 && z % 2 != 0 || x % 2 != 0 && z % 2 == 0) ? Color.white : Color.gray);
                    tile.transform.SetParent(GridParent);
                }
            }
        }

        public Tile GetTile(Vector2Int position)
        {
            return _grid.GetGridObject(position);
        }
    }
}