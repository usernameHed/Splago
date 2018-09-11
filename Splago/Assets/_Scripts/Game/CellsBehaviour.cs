using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;

[TypeInfoBox("Behavior of a cell")]
public class CellsBehaviour : MonoBehaviour
{
    [SerializeField]
    private Image image;
    public Transform GetParentCursor() { return (transform); }

    [ShowInInspector, ReadOnly]
    private int xPos;
    [ShowInInspector, ReadOnly]
    private int yPos;
    [ShowInInspector, ReadOnly]
    private CellData gridData;

    here create cross where we already click...

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
                GridEditor.Instance.HoverCase(xPos, yPos, this, true);
        }
        else
            GameLoop.Instance.HoverCase(xPos, yPos, this, true);
    }

    public void PointerExit()
    {
        if (GridEditor.Instance.IsInEditor())
        {
            if (Input.GetMouseButton(0))
                PointerClick();
            GridEditor.Instance.HoverCase(xPos, yPos, this, false);
        }            
        else
            GameLoop.Instance.HoverCase(xPos, yPos, this, false);
    }

    public void PointerClick()
    {
        if (GridEditor.Instance.IsInEditor())
            GridEditor.Instance.ClickOnCase(xPos, yPos);
        else
            GameLoop.Instance.ClickOnCase(xPos, yPos);
    }
}
