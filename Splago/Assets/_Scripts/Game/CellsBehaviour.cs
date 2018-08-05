using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;

public class CellsBehaviour : MonoBehaviour
{
    [SerializeField]
    private Image image;

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
        image.sprite = grid.sprite;
        image.color = grid.color;
    }

    /// <summary>
    /// called by eventTrigger on case
    /// </summary>
    public void PointerEnter()
    {
        if (GridEditor.Instance.IsInEditor())
        {
            if (Input.GetMouseButton(0))
                PointerClick();
            else
                GridEditor.Instance.OverCase(xPos, yPos, transform);
        }            
        else
            GameLoop.Instance.OverCase(xPos, yPos, transform);
    }

    public void PointerExit()
    {
        if (GridEditor.Instance.IsInEditor())
        {
            if (Input.GetMouseButton(0))
                PointerClick();
            GridEditor.Instance.OverExitCase(xPos, yPos);
        }            
        else
            GameLoop.Instance.OverExitCase(xPos, yPos);
    }

    public void PointerClick()
    {
        if (GridEditor.Instance.IsInEditor())
            GridEditor.Instance.ClickOnCase(xPos, yPos);
        else
            GameLoop.Instance.ClickOnCase(xPos, yPos);
    }
}
