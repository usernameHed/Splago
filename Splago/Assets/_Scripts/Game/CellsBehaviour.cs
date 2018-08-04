using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class CellsBehaviour : MonoBehaviour
{
    private GridData gridData;
    [ShowInInspector]
    private int xPos;
    [ShowInInspector]
    private int yPos;

    public void Init(GridData grid, int x, int y)
    {
        gridData = grid;
        xPos = x;
        yPos = y;
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
