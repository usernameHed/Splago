using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[Serializable]
public struct InsideCell
{
    public Point posCell;
    public Transform cell;
}

public class CursorGrid : MonoBehaviour
{
    #region type data
    public enum CursorEnum
    {
        HoverSimple,
        InEditor,
    }

    [Serializable]
    public struct CursorStruct
    {
        public CursorEnum type;
        public GameObject obj;
    }

    /// <summary>
    /// get the cursor type, based on the enum
    /// </summary>
    private int GetCursorType(CursorEnum typeToEnable)
    {
        for (int i = 0; i < cursors.Count; i++)
        {
            if (cursors[i].type == typeToEnable)
                return (i);
        }
        Debug.Log("wrong cursor type");
        return (-1);
    }

    #endregion

    [SerializeField]
    private List<CursorStruct> cursors;

    //TODO
    //here handle creation of cursors at certain position & destruction ?

    [SerializeField]
    private Transform posOfCursors;
    //[SerializeField, ReadOnly]
    //private List<InsideCell> insideCellList;

    [SerializeField]
    private GameObject prefabsCellInside;

    [ShowInInspector, ReadOnly]
    private int indexCursor = 0;

 
    /// <summary>
    /// initialize the cursor with a type
    /// </summary>
    public void Init(CursorEnum typeToEnable)
    {
        posOfCursors.ActiveAllChild(false);
        indexCursor = GetCursorType(typeToEnable);
    }

    public void OverCase(CellsBehaviour cell, bool hover)
    {
        if (hover)
        {
            posOfCursors.ActiveAllChild(false);

            //Debug.Log("change scale");
            cursors[indexCursor].obj.transform.SetParent(cell.GetParentCell());
            cursors[indexCursor].obj.GetComponent<RectTransform>().ResetPos();
            cursors[indexCursor].obj.GetComponent<RectTransform>().ResetScale();
            cursors[indexCursor].obj.SetActive(true);
        }
        else
        {
            cursors[indexCursor].obj.SetActive(false);
            cursors[indexCursor].obj.transform.SetParent(posOfCursors);
        }
    }

    [Button]
    public void AddToCell(int x, int y, string idName, bool duplicate = true)
    {
        AddToCell(x, y, GridDatas.Instance.GetIdByName(idName), duplicate);
    }

    /// <summary>
    /// called when we want to add something to a cell
    /// (add a image on top of a cell)
    /// </summary>
    private void AddToCell(int x, int y, ushort id, bool duplicate = true)
    {
        CellsBehaviour _cell = GridManager.Instance.GetCell(x, y);
        if (!duplicate && _cell.OtherWithSameId(id))
            return;

        /*
        InsideCell inside = new InsideCell
        {
            cell = _cell.transform,
            posCell = new Point(x, y),
        };
        insideCellList.Add(inside);
        */

        GameObject newCell = Instantiate(prefabsCellInside, Vector3.zero, Quaternion.identity, _cell.GetParentCell());
        newCell.GetComponent<RectTransform>().ResetPos();
        newCell.GetComponent<RectTransform>().ResetScale();
        newCell.SetActive(true);

        _cell.AddToCell(newCell, x, y, id);
    }

    [Button]
    public void DeleteCell(int x, int y, string idName, bool duplicate = true)
    {
        DeleteCell(x, y, GridDatas.Instance.GetIdByName(idName), duplicate);
    }
    /// <summary>
    /// called to delete something inside a cell
    /// </summary>
    private void DeleteCell(int x, int y, ushort id, bool all = false)
    {
        CellsBehaviour _cell = GridManager.Instance.GetCell(x, y);

        _cell.DeleteToCell(x, y, id, all);


        /*


        InsideCell inside = new InsideCell
        {
            cell = _cell.transform,
            posCell = new Point(x, y),
        };
        insideCellList.Remove(inside);
        */
    }

    [Button]
    public void DeleteAllInCell(int x, int y)
    {
        CellsBehaviour _cell = GridManager.Instance.GetCell(x, y);

        _cell.DeleteAllInCell(x, y);
    }
}
