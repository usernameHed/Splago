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
    private string mapToLoadAtStart = "map1";

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
            LoadNewGrid(sizeX, sizeY, gridData);
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
