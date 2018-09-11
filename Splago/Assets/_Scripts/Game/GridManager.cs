using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;

/// <summary>
/// gère les datas & l'affichage de la grille
/// </summary>
[TypeInfoBox("Manager creation of grid")]
public class GridManager : SingletonMono<GridManager>
{
    [Tooltip("taille X du tableau"), OnValueChanged("InitGrid"), SerializeField]
    private int sizeX = 10;
    public int SizeX { get { return (sizeX); } }
    [Tooltip("taille Y du tableau"), OnValueChanged("InitGrid"), SerializeField]
    private int sizeY = 10;
    public int SizeY { get { return (sizeY); } }

    [HideInInspector]
    public ushort[,] gridData;

    [SerializeField]
    private string mapToLoadAtStart = "default";

    [SerializeField]
    private GridSaveAndLoad gridSaveAndLoad;

    [ShowInInspector]
    private GameObject[,] gridcells;

    public Transform grid;
    public GameObject gridCellPrefabs;
    public GridScalar gridScalar;
    public GridLayoutGroup gridLayout;

    /// <summary>
    /// permet d'initialiser la gille (les data & l'affichage)
    /// </summary>
    [Button]
    public void InitGrid()
    {
        gridData = new ushort[sizeX, sizeY];
        gridSaveAndLoad.Init();

        if (!gridSaveAndLoad.Load(mapToLoadAtStart))
        {
            Debug.LogWarning("Load new grid, because loading default failed !");
            LoadNewGrid(sizeX, sizeY, gridData);
        }
            
    }

    /// <summary>
    /// clear the grid !
    /// </summary>
    public void ClearGrid()
    {
        gridData = new ushort[sizeX, sizeY];
        LoadNewGrid(sizeX, sizeY, gridData);
    }

    /// <summary>
    /// here load new grid (x, y and data)
    /// </summary>
    public void LoadNewGrid(int newSizeX, int newSizeY, ushort[,] newGridData)
    {
        //setup new datas
        sizeX = newSizeX;
        sizeY = newSizeY;
        gridData = newGridData;

        grid.ClearChild();
        gridcells = new GameObject[sizeX, sizeY];

        gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayout.constraintCount = sizeX;

        for (int i = 0; i < gridData.GetLength(1); i++)
        {
            for (int j = 0; j < gridData.GetLength(0); j++)
            {
                gridcells[j, i] = Instantiate(gridCellPrefabs, grid);
                FillCase(j, i, gridData[j, i]);
            }
        }

        gridScalar.Init(sizeX, sizeY);
    }

    /// <summary>
    /// test if a player can place a spells
    /// </summary>
    public bool IsSameTypeCellAndPlayer(int x, int y, ushort realIndexPlayer)
    {
        return (gridData[x, y] == realIndexPlayer);
    }

    //change spawn with player
    public void SpawnInit()
    {
        int indexPlayer = 0;
        int indexSpawn = GridDatas.Instance.GetIdByName(GridDatas.Instance.nameSpawn);

        for (int i = 0; i < gridData.GetLength(1); i++)
        {
            for (int j = 0; j < gridData.GetLength(0); j++)
            {
                
                if (gridData[j, i] == indexSpawn)
                {
                    ushort realIndexPlayer = GameLoop.Instance.GetOrderPlayer()[indexPlayer].GetRealIndex();
                    Debug.Log("here spawn to change to index: " + indexPlayer + ", " + realIndexPlayer);
                    FillCase(j, i, realIndexPlayer);
                    indexPlayer++;
                }
            }
        }
    }

    public void SetSpellsHover(ushort realIndex, SpellType spellType, int levelSpell, int x, int y, bool hover)
    {
        
    }

    public void SetSpells(ushort realIndex, SpellType spellType, int levelSpell, int x, int y)
    {
        Debug.Log("create spell on: " + x + ", " + y);
        Spells spell = SpellsManager.Instance.GetSpellByType(spellType);
        bool[,] arraySpell = spell.GetLevel(levelSpell);

        
        for (int i = 0; i < arraySpell.GetLength(1); i++)   //y
        {
            for (int j = 0; j < arraySpell.GetLength(0); j++)   //x
            {
                if (arraySpell[j, i])
                {
                    //here test if we can fill on the map
                    int realXX = (x + j) - (arraySpell.GetLength(0) / 2);
                    int realYY = (y + i) - (arraySpell.GetLength(1) / 2);
                    
                    //Debug.Log("real x:" + realYY);
                    //Debug.Log("real y:" + realXX);

                    //TryToSetPlayerSpellOnCell(j, i, realIndex);
                    TryToSetPlayerSpellOnCell(realXX, realYY, realIndex);
                }
            }
        }

        /*
        int iArray = 0;
        int jArray = 0;
        for (int i = y - (arraySpell.GetLength(1) / 2); i < y + (arraySpell.GetLength(1) / 2); i++)   //y
        {
            for (int j = x - (arraySpell.GetLength(0) / 2); j < x + (arraySpell.GetLength(0) / 2); j++)   //x
            {
                if (i < 0 || i >= sizeY)
                {
                    iArray++;
                    continue;
                }
                    
                if (j < 0 || j >= sizeX)
                {
                    iArray++;
                    continue;
                }
                    
                Debug.Log("sizeXMAx: " + sizeX + ", sizeYMAx" + sizeY);
                Debug.Log("x: " + j + ", y: " + i);
                Debug.Log("real x spell: " + iArray + ", y: " + jArray);

                //if we have to fill
                if (arraySpell[iArray, jArray])
                {
                    //here test if we can fill on the map
                    TryToSetPlayerSpellOnCell(j, i, realIndex);
                }

                iArray++;
            }
            jArray++;
        }
        */
    }

    /// <summary>
    /// is this cell other player than me ?
    /// </summary>
    private bool IsPlayer(ushort indexPlayer)
    {
        for (int i = 0; i < GridDatas.Instance.cellsDatasPlayer.Count; i++)
        {
            if (indexPlayer == GridDatas.Instance.cellsDatasPlayer[i].id)
            {
                return (true); //here same player
            }
        }
        return (false);
    }

    /// <summary>
    /// can we erase cell with our color ?
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="realIndex"></param>
    private void TryToSetPlayerSpellOnCell(int x, int y, ushort realIndex)
    {
        Debug.Log("x: " + x);
        Debug.Log("y: " + y);
        if (x < 0 || x >= sizeX)
            return;
        if (y < 0 || y >= sizeY)
            return;

        ushort indexCase = gridData[x, y];

        //if the case we want to touch is a player...
        if (IsPlayer(indexCase))
        {
            //if this is our player, just rewhite
            if (indexCase == realIndex)
            {
                FillCase(x, y, realIndex);
                return;
            }
            //if this is a player, but not our... create wall !!
            else
            {
                FillCase(x, y, GridDatas.Instance.GetIdByName(GridDatas.Instance.nameWall));
                return;
            }
        }

        switch (indexCase)
        {
            case 0: //empty case
                FillCase(x, y, realIndex);
                break;
            
            default:
                //do nothing
                break;
        }
    }

    /// <summary>
    /// get all tab data in string
    /// </summary>
    /// <returns></returns>
    public string GetAllDataToString()
    {
        string dataToSend = "";
        for (int i = 0; i < gridData.GetLength(1); i++)
        {
            for (int j = 0; j < gridData.GetLength(0); j++)
            {
                dataToSend += gridData[j, i];
                if (j < gridData.GetLength(0) - 1)
                    dataToSend += " ";
            }
            if (i < gridData.GetLength(1) - 1)
                dataToSend += "\n";
        }
        return (dataToSend);
    }

    /// <summary>
    /// here fill a case, and update his UI
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="data"></param>
    public void FillCase(int x, int y, ushort data)
    {
        gridData[x, y] = data;
        CellsBehaviour cellBehave = gridcells[x, y].GetComponent<CellsBehaviour>();
        cellBehave.Init(x, y, GridDatas.Instance.GetCellsByData(data));
    }
}
