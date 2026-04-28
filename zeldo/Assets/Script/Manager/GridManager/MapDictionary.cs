using System.Collections.Generic;
using UnityEngine;

public class MapDictionary : MonoBehaviour
{
    private static MapDictionary instance;
    public static MapDictionary Instance => instance;
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
    [SerializeField] private Camera mainCamera;
    public int gridSizeX;
    public int gridSizeY;
    private Vector3 firstCell;
    [SerializeField] private Grid mapGrid;
    public Dictionary<(float, float), int> mapDictionary;
    void Start()
    {
        CreateDictionary(gridSizeX,gridSizeY);
    }
    private void CreateDictionary(int gridSizeX,int gridSizeY)
    {
        GetNewFirstDictionaryCell();
        if (mapDictionary == null)
        {
            mapDictionary = new Dictionary<(float, float), int>();
        }
        if(mapDictionary.ContainsKey((firstCell.x,firstCell.y)) == false)
        {            
             for (int i = 0; i < gridSizeY; i++)
             {
                 for(int j = 0; j < gridSizeX; j++)
                 {
                     mapDictionary.Add((firstCell.x+j,firstCell.y-i), j);
                     print(firstCell.x + j);
                     print(firstCell.y-i);
                 }
             }
        }
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
}
