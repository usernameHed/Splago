using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;

/// <summary>
/// gère les datas & l'affichage de la grille
/// </summary>
public class GridManager : SingletonMono<GridManager>
{
    [Tooltip("taille X du tableau"), OnValueChanged("InitGrid"), SerializeField]
    private int sizeX = 10;
    [Tooltip("taille Y du tableau"), OnValueChanged("InitGrid"), SerializeField]
    private int sizeY = 10;

    private ushort[,] gridData;

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
        grid.ClearChild();
        gridData = new ushort[sizeX, sizeY];
        gridcells = new GameObject[sizeX, sizeY];

        gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayout.constraintCount = sizeX;

        for (int i = 0; i < gridData.GetLength(1); i++)
        {
            for (int j = 0; j < gridData.GetLength(0); j++)
            {
                /*
                gridData[j, i] = 0;
                gridcells[j, i] = Instantiate(gridCellPrefabs, grid);
                CellsBehaviour cellBehave = gridcells[j, i].GetComponent<CellsBehaviour>();
                cellBehave.Init(j, i, GridDatas.Instance.GetCellsByData(0));
                */
                gridcells[j, i] = Instantiate(gridCellPrefabs, grid);
                FillCase(j, i, 0);

            }
        }

        gridScalar.Init(sizeX, sizeY);
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
