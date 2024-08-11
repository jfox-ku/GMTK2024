using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid<T>
{
    private int width;
    private int depth;
    private readonly float _cellSize;
    private T[,] gridArray;
    
    public Grid(int width, int depth, float cellSize)
    {
        this.width = width;
        this.depth = depth;
        _cellSize = cellSize;
        gridArray = new T[width, depth];
        Debug.Log("Grid created with width: " + width + " and depth: " + depth);

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int z = 0; z < gridArray.GetLength(1); z++)
            {
                Debug.DrawLine(GetWorldPosition(x,z),GetWorldPosition(x,z+1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x,z),GetWorldPosition(x+1,z), Color.white, 100f);
            }
        }
        
        Debug.DrawLine(GetWorldPosition(0,depth),GetWorldPosition(width,depth), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width,0),GetWorldPosition(width,depth), Color.white, 100f);
    }
    
    public Vector3 GetWorldPosition(int x, int z)
    {
        return new Vector3(x, 0, z) * _cellSize - new Vector3(width/2f, 0, depth/2f) * _cellSize;
    }

    public T GetGridObject(Vector2Int pos)
    {
        return GetGridObject(pos.x, pos.y);
    }
    public T GetGridObject(int x, int z)
    {
        if (x >= 0 && z >= 0 && x < width && z < depth)
        {
            return gridArray[x, z];
        }
        else
        {
            return default(T);
        }
    }
    
}
