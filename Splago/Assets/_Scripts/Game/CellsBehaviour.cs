using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class CellsBehaviour : MonoBehaviour
{
    [ShowInInspector, ReadOnly]
    private int xPos;
    [ShowInInspector, ReadOnly]
    private int yPos;
    [ShowInInspector, ReadOnly]
    private CellData gridData;

    public void Init(int x, int y, CellData grid)
    {
        xPos = x;
        yPos = y;
        gridData = grid;
    }

    public void PointerEnter()
    {
        Debug.Log("PointerEnter");
    }

    public void PointerExit()
    {
        Debug.Log("PointerExit");
    }

    public void PointerUp()
    {
        Debug.Log("PointerUp");
    }

    public void PointerClick()
    {
        Debug.Log("PointerClick");
    }
}
