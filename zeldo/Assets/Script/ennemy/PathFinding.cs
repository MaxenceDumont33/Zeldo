using System;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    private static PathFinding instance;
    public static PathFinding Instance => instance;

    private void Awake()
    {
        if (instance)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    MapDictionary mapDictionaryRef;
    LayerMask detectedLayer;
    public struct Cell
    {
        public double g;
        public double h;
        public double f;
        public float x;
        public float y;
    }
    Cell start;
    public List<Cell> openList;
    public List<Cell> closedList;
    private void Start()
    {
        mapDictionaryRef = MapDictionary.Instance;
        openList = new List<Cell>();
        closedList = new List<Cell>();
        start = new Cell();
        start.g = 0;
        start.h = 0;
        start.f = start.g + start.h;
        openList.Add(start);
    }

    public void StartLookingForPath(Vector2 enemyPos,Vector2 playerPos)
    {
        LookForPath(enemyPos,playerPos);
    }
    private void LookForPath(Vector2 enemyPos , Vector2 destinationPos)
    {
        float Row = mapDictionaryRef.gridXMaxValue;
        float Col = mapDictionaryRef.gridYMaxValue;
        (enemyPos.x,enemyPos.y) = mapDictionaryRef.GetCellCenter(enemyPos.x, enemyPos.y);
        if (!IsValid(enemyPos.x,enemyPos.y,Row,Col) || !IsValid(destinationPos.x, destinationPos.y,Row,Col))
        {
            print("error, point out of map");
            return;
        }
        // peut pauser problème
        if (! IsUnBlocked(enemyPos.x, enemyPos.y) || ! IsUnBlocked(destinationPos.x, destinationPos.y))
        {
            print(" error is blocked");
        }
        if (enemyPos.x == destinationPos.x && enemyPos.y == destinationPos.y)
        {
            Console.WriteLine("We are already at the destination");
            return;
        }
        bool[,] closedList = new bool[mapDictionaryRef.gridSizeX, mapDictionaryRef.gridSizeY];
        Cell [,] cellDetails = new Cell[mapDictionaryRef.gridSizeX, mapDictionaryRef.gridSizeY];

        for (int i = 0; i < mapDictionaryRef.gridSizeX; i++)
        {
            for (int j = 1; j < mapDictionaryRef.gridSizeY; j++)
            {
                cellDetails[i, j].f = double.MaxValue;
                cellDetails[i, j].g = double.MaxValue;
                cellDetails[i, j].h = double.MaxValue;
                cellDetails[i, j].x = -1;
                cellDetails[i, j].y = -1;
            }
        }
         int x = (int)Mathf.Ceil(enemyPos.x + 9), y = (int)Mathf.Round(enemyPos.y+5);
        cellDetails[x, y].f = 0.0;
        cellDetails[x, y].g = 0.0;
        cellDetails[x, y].h = 0.0;
        cellDetails[x, y].x = x + 0.5f;
        cellDetails[x, y].y = y + 0.5f;

        SortedSet<(double, Pair)> openList = new SortedSet<(double, Pair)>(
            Comparer<(double, Pair)>.Create((a, b) => a.Item1.CompareTo(b.Item1)));
    }


    private bool LookIfWall(Vector2 startCell, Vector2 endCell)
    {
        if(Physics2D.Raycast(startCell, (startCell - endCell), 1, detectedLayer) == true)
        {
            return true;
        }        
        return false;
    }
    public bool IsValid(float row, float col, float ROW, float COL)
    {
        return (row >= (ROW - mapDictionaryRef.gridSizeX)) && (row < ROW) && (col >= COL - mapDictionaryRef.gridSizeY) && (col < COL);
    }
    public bool IsUnBlocked(float row, float col)
    {
        mapDictionaryRef.mapDictionary.TryGetValue((row, col) ,out MapDictionary.cell outCell);
        if(outCell.hisBlocked == false)
        {
            return true;
        }
        return false;
    }

}
