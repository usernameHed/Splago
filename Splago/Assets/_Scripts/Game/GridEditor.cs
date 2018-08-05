using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class GridEditor : SingletonMono<GridEditor>
{
    [SerializeField]
    private GridEditorUI gridEditorUi;
    
    public GridSaveAndLoad gridSaveAndLoad;

    [Tooltip("active l'édition"), OnValueChanged("SetEditorMode"), SerializeField]
    private bool activeEdition = false;
    public void SetEdition(bool active) { activeEdition = active; SetEditorMode(); }
    public bool IsInEditor() { return (activeEdition); }


    [Tooltip("id to change edit"), SerializeField, ReadOnly]
    private ushort idToChange;
    public void SetIdUsh(int ush) { idToChange = (ushort)ush; }

    [SerializeField]
    private GameObject cursorEditor;

    private void Start()
    {
        cursorEditor.SetActive(false);

        activeEdition = false;
        SetEditorMode();
        gridEditorUi.InitDropDown();
    }

    

    /// <summary>
    /// set editor mode or not
    /// </summary>
    private void SetEditorMode()
    {
        if (activeEdition)
        {
            Debug.Log("editor mode");
            InitNewMap();
            
        }            
        else
        {
            Debug.Log("no more editor mode");
            gridEditorUi.ActiveDevMod(false);
        }
    }

    /// <summary>
    /// when activating editorMode, setup the map
    /// </summary>
    [Button]
    public void InitNewMap()
    {
        GridManager.Instance.InitGrid();
        gridEditorUi.ActiveDevMod(true);
    }

    /// <summary>
    /// here exlicitly clear the grid
    /// </summary>
    public void ClearGrid()
    {
        GridManager.Instance.ClearGrid();
    }

    /// <summary>
    /// generate a map ??
    /// </summary>
    public void Generate()
    {
        Debug.Log("Generate");
    }

    /// <summary>
    /// the player is over this case
    /// </summary>
    public void OverCase(int x, int y, Transform parent)
    {
        //Debug.Log("[editor] over " + x + ", " + y);
        
        cursorEditor.transform.SetParent(parent);
        cursorEditor.GetComponent<RectTransform>().ResetPos();
        cursorEditor.SetActive(true);
    }
    public void OverExitCase(int x, int y)
    {
        //Debug.Log("[editor] over " + x + ", " + y);
        cursorEditor.SetActive(false);
        cursorEditor.transform.SetParent(transform);
    }
    /// <summary>
    /// the player clic on this case
    /// </summary>
    public void ClickOnCase(int x, int y)
    {
        Debug.Log("[editor] click " + x + ", " + y);
        GridManager.Instance.FillCase(x, y, idToChange);
    }

    /// <summary>
    /// here if we change the id's cells data in gridData,
    /// change the saved map if there are some
    /// (for not broke the saved map)
    /// </summary>
    /// <param name="oldSave">old data</param>
    /// <param name="newDatas">new data</param>
    public void DataGridChanged(List<CellData> oldSave, List<CellData> newDatas)
    {
        Debug.Log("here change the saved map with new id data !");
        gridEditorUi.InitDropDown();
    }
}
