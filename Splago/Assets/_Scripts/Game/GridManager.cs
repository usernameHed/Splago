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

    private List<Point> actionPlayer = new List<Point>();
    private List<Point> actionSpecialPlayer = new List<Point>();

    private List<ushort> previousActionPlayer = new List<ushort>();
    private List<ushort> previousSpecialActionPlayer = new List<ushort>();

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
                    ushort realIndexPlayer = GameLoop.Instance.GetOrderPlayer()[indexPlayer].GetRealIndexNumber(0);
                    Debug.Log("here spawn to change to index: " + indexPlayer + ", " + realIndexPlayer);
                    FillCase(j, i, realIndexPlayer);
                    indexPlayer++;
                }
            }
        }
    }

    /// <summary>
    /// fill old value, and clear
    /// </summary>
    public void ClearListLast(bool justList = false)
    {
        if (justList)
        {
            actionPlayer.Clear();
            previousActionPlayer.Clear();

            actionSpecialPlayer.Clear();
            previousSpecialActionPlayer.Clear();
            return;
        }

        for (int i = 0; i < actionPlayer.Count; i++)
        {
            FillCase(actionPlayer[i].x, actionPlayer[i].y, previousActionPlayer[i]);
            
        }
        for (int i = 0; i < actionSpecialPlayer.Count; i++)
        {
            GameLoop.Instance.cursor.DeleteCell(actionSpecialPlayer[i].x, actionSpecialPlayer[i].y, "WallHover");
        }

        actionPlayer.Clear();
        previousActionPlayer.Clear();
        actionSpecialPlayer.Clear();
        previousSpecialActionPlayer.Clear();
    }

    public void SetSpells(PlayerManager player, SpellType spellType, int levelSpell, int x, int y, int levelTypePlayer)
    {
        Debug.Log("create spell on: " + x + ", " + y);

        Spells spell = SpellsManager.Instance.GetSpellByType(spellType);
        bool[,] arraySpell = spell.GetLevelForArray(levelSpell);

        
        for (int i = 0; i < arraySpell.GetLength(1); i++)   //y
        {
            for (int j = 0; j < arraySpell.GetLength(0); j++)   //x
            {
                if (arraySpell[j, i])
                {
                    //here test if we can fill on the map
                    int realXX = (x + j) - (arraySpell.GetLength(0) / 2);
                    int realYY = (y + i) - (arraySpell.GetLength(1) / 2);
                    
                    if (TryToSetPlayerSpellOnCell(realXX, realYY, player, levelTypePlayer))
                    {
                        
                    }
                }
            }
        }
    }

    /// <summary>
    /// is this cell other player than me ?
    /// TODO: if we touch poison, or something else...
    /// </summary>
    private bool IsPlayer(ushort cellDataIndex)
    {
        for (int i = 0; i < GridDatas.Instance.cellsDatasPlayer.Count; i++)
        {
            if (cellDataIndex == GridDatas.Instance.cellsDatasPlayer[i].colorPlayer[0].id)    //tester seulement la version definitive...
            {
                return (true);  //here we have a player
            }
        }
        return (false);
    }

    private bool IsSamePlayer(ushort cellDataIndex, PlayerManager playerAttacking)
    {
        if (!IsPlayer(cellDataIndex))
            return (false);

        for (int i = 0; i < GridDatas.Instance.cellsDatasPlayer.Count; i++)
        {
            if (cellDataIndex == GridDatas.Instance.cellsDatasPlayer[i].colorPlayer[0].id)    //tester seulement la version definitive...
            {
                if (playerAttacking.GetIndex() == i)
                {
                    //here same player than playerAttacking
                    return (true);
                }
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
    private bool TryToSetPlayerSpellOnCell(int x, int y, PlayerManager player, int levelTypePlayer)
    {
        //Debug.Log("x: " + x);
        //Debug.Log("y: " + y);
        if (x < 0 || x >= sizeX)
            return (false);
        if (y < 0 || y >= sizeY)
            return (false);

        ushort indexCase = gridData[x, y];

        ushort indexToSet = player.GetRealIndexNumber(levelTypePlayer);



        //if the case we want to touch is a player...
        if (IsPlayer(indexCase))
        {
            //if this is our player, just rewhite
            if (IsSamePlayer(indexCase, player))
            {
                //FillCase(x, y, indexToSet);
                return (false);
            }
            //if this is a player, but not our... create wall !!
            else
            {
                //change wall only if definitive !!!
                if (levelTypePlayer == 0)
                {
                    FillCase(x, y, GridDatas.Instance.GetIdByName(GridDatas.Instance.nameWall));
                    GameLoop.Instance.cursor.DeleteCell(x, y, "PlayerClicked");
                    return (true);
                }
                else if (levelTypePlayer == 1)
                {
                    //maybe create an other symbole on the cell ?
                    GameLoop.Instance.cursor.AddToCell(x, y, "WallHover");

                    actionSpecialPlayer.Add(new Point(x, y));
                    previousSpecialActionPlayer.Add(GridDatas.Instance.GetIdByName("WallHover"));
                }


                return (false);
            }
        }

        switch (indexCase)
        {
            case 0: //empty case

                actionPlayer.Add(new Point(x, y));
                previousActionPlayer.Add(gridData[x, y]);

                FillCase(x, y, indexToSet);
                break;
            
            default:
                //do nothing
                break;
        }
        return (false);
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
        CellsBehaviour cellBehave = GetCell(x, y);
        cellBehave.Init(x, y, GridDatas.Instance.GetCellsByData(data));
    }

    /// <summary>
    /// return cell with x, y
    /// </summary>
    public CellsBehaviour GetCell(int x, int y)
    {
        return (gridcells[x, y].GetComponent<CellsBehaviour>());
    }
}
