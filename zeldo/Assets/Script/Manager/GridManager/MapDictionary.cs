using System.Collections.Generic;
using UnityEngine;

public class MapDictionary : MonoBehaviour
{
    private static MapDictionary instance;
    public static MapDictionary Instance => instance;

    public struct cell 
    {
        public bool up;
        public bool left;
        public bool right;
        public bool down;
        public bool hasBeenRead;
        public bool hisBlocked;
    }
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
    [SerializeField] public Camera mainCamera;
    public int gridSizeX;
    public int gridSizeY;

    public float gridXMaxValue;
    public float gridYMaxValue;
    public float gridXMinValue;
    public float gridYMinValue;

    private Vector3 firstCell;
    [SerializeField] private Grid mapGrid;
    cell cellStruct;
    public Dictionary<(float, float),cell> mapDictionary;
    void Start()
    {
        CreateDictionary(gridSizeX,gridSizeY);
    }
    private void CreateDictionary(int gridSizeX,int gridSizeY)
    {
        GetNewFirstDictionaryCell();
        if (mapDictionary == null)
        {
            mapDictionary = new Dictionary<(float, float),cell>();          
        }
        if(mapDictionary.ContainsKey((firstCell.x,firstCell.y)) == false)
        {            
             for (int i = 0; i < gridSizeY; i++)
             {
                 for(int j = 0; j < gridSizeX; j++)
                 {
                     mapDictionary.Add((firstCell.x+j,firstCell.y-i),cellStruct);
                     print(firstCell.x + j);
                     print(firstCell.y-i);
                 }
             }
        }
        gridXMaxValue = (gridSizeX / 2) + mainCamera.transform.position.x;
        gridXMinValue = (-gridSizeX / 2) + mainCamera.transform.position.x; 
        gridYMaxValue = (gridSizeY / 2) + mainCamera.transform.position.y;
        gridYMinValue = (-gridSizeY / 2) + mainCamera.transform.position.y;
        print(gridXMaxValue + "," + gridXMinValue);
        print(gridYMaxValue + "," + gridYMinValue);

    }
    public void RefreshDictionary()
    {
        CreateDictionary(gridSizeX,gridSizeY);
    }
    private void GetNewFirstDictionaryCell()
    {
        firstCell = mapGrid.WorldToCell(mainCamera.transform.position + (new Vector3(-gridSizeX / 2 + 0.2f, gridSizeY / 2 - 0.2f, 0)));
        firstCell = mapGrid.GetCellCenterWorld(new Vector3Int((int)firstCell.x, (int)firstCell.y, (int)firstCell.z));
        print(firstCell);
    }
    public (float,float) GetCellCenter(float enemyPosX,float ennemyPosY)
    {
        Vector3 cellCenter = mapGrid.GetCellCenterWorld(new Vector3Int((int)enemyPosX, (int)ennemyPosY, 0));
        float cellCenterX = cellCenter.x;
        float cellCenterY = cellCenter.y;
        return (cellCenterX ,  cellCenterY);
    }
}
