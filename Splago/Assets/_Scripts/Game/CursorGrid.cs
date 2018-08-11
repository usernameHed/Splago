using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

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

    [SerializeField]
    private Transform posOfCursors;

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

            cursors[indexCursor].obj.transform.SetParent(cell.GetParentCursor());
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
}
