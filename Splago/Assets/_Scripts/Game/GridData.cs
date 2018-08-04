using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;

public class GridData : MonoBehaviour
{
    [OnValueChanged("InitGrid")]
    public int sizeX = 10;
    [OnValueChanged("InitGrid")]
    public int sizeY = 10;

    public ushort[,] gridData;
    [ShowInInspector]
    public GameObject[,] gridcells;

    public Transform grid;
    public GameObject gridCellPrefabs;
    public GridScalar gridScalar;
    public GridLayoutGroup gridLayout;

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
                gridData[j, i] = 0;
                gridcells[j, i] = Instantiate(gridCellPrefabs, grid);
                CellsBehaviour cellBehave = gridcells[j, i].GetComponent<CellsBehaviour>();
                cellBehave.Init(this, j, i);
            }
        }

        gridScalar.Init(sizeX, sizeY);
    }

    public void FillCase(int x, int y, ushort data)
    {
        gridData[y, x] = data;
    }
}
