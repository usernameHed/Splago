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
    public Transform GetParentCell() { return (transform); }

    [ShowInInspector, ReadOnly]
    private int xPos;
    [ShowInInspector, ReadOnly]
    private int yPos;
    [ShowInInspector, ReadOnly]
    private CellData gridData;

    [ReadOnly]
    public bool isClicked = false;

    [ReadOnly]
    public List<CellsId> childHover;

    //TODO:
    //here create cross where we already click...

    public void Init(int x, int y, CellData grid)
    {
        xPos = x;
        yPos = y;
        gridData = grid;
        image.sprite = grid.sprite;
        image.color = grid.color;
    }

    /// <summary>
    /// si il y a déja un enfant du meme id, retourne vrai
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool OtherWithSameId(ushort id)
    {
        for (int i = 0; i < childHover.Count; i++)
        {
            if (childHover[i].GetComponent<CellsId>()
                && childHover[i].GetComponent<CellsId>().GetId() == id)
            {
                return (true);
            }
        }
        return (false);
    }

    public void AddToCell(GameObject newChild, int x, int y, ushort id)
    {
        //test if other child, with the same id exist ? or not ?
        CellsId newCellId = newChild.GetComponent<CellsId>();
        newCellId.SetIdAndImage(id);

        childHover.Add(newCellId);
    }

    public void DeleteToCell(int x, int y, ushort id, bool all = false)
    {
        Debug.Log("try to delete id: " + id + ",(" + x + " " + y + ")");
        bool done = false;
        foreach (Transform child in GetParentCell())
        {
            if (child.GetComponent<CellsId>()
                && child.GetComponent<CellsId>().GetId() == id)
            {
                childHover.Remove(child.GetComponent<CellsId>());
                if (!done)
                {
                    Destroy(child.gameObject);
                    done = true;
                }

                if (!all)
                    return;
            }
            //Something(child.gameObject);
        }
        //delete [all] cell child [id]
    }

    public void DeleteAllInCell(int x, int y)
    {
        GetParentCell().ClearChild();//NOP: not cursor !!!
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
