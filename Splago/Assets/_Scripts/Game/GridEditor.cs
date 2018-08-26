using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[TypeInfoBox("Used to draw custom map, save & load")]
public class GridEditor : SingletonMono<GridEditor>
{
    [SerializeField]
    private bool activeEditor = true; 

    [SerializeField]
    private GameObject[] ButtonActiveEditor;

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

    public void Init()
    {
        ButtonActiveEditor[0].SetActive(activeEditor);
        ButtonActiveEditor[1].SetActive(activeEditor);

        activeEdition = false;
        SetEditorMode();
        gridEditorUi.InitDropDown();
        gridSaveAndLoad.Init();
    }
    

    /// <summary>
    /// set editor mode or not
    /// </summary>
    private void SetEditorMode()
    {
        if (activeEdition)
        {
            Debug.Log("editor mode");
            GameLoop.Instance.cursor.Init(CursorGrid.CursorEnum.InEditor);
            InitNewMap();
        }
        else
        {
            //Debug.Log("no more editor mode");
            GameLoop.Instance.InitMisc();
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
    public void HoverCase(int x, int y, CellsBehaviour cell, bool hover)
    {
        //Debug.Log("[editor] over " + x + ", " + y);

        GameLoop.Instance.cursor.OverCase(cell, hover);
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
        return;

        string tmpFile = "[DataChange]tmp";

        Debug.Log("first, init the grid (in case of inspector)");
        GridManager.Instance.InitGrid();

        Debug.Log("first create a tmp DataChangetmp");
        gridSaveAndLoad.Save(tmpFile);

        Debug.Log("then init the fills saved");
        gridSaveAndLoad.Init(); //load les fichier
        Debug.Log("here change the saved map with new id data !");
        for (int k = 0; k < gridSaveAndLoad.savedFiles.Count; k++)
        {
            //load un fichier...
            Debug.Log("load: " + gridSaveAndLoad.savedFiles[k]);

            gridSaveAndLoad.Load(gridSaveAndLoad.savedFiles[k]);
            for (int i = 0; i < GridManager.Instance.gridData.GetLength(1); i++)
            {
                for (int j = 0; j < GridManager.Instance.gridData.GetLength(0); j++)
                {
                    if (GridManager.Instance.gridData[j, i] == 0)
                        continue;
                    
                    for (int m = 0; m < oldSave.Count; m++)
                    {
                        if (GridManager.Instance.gridData[j, i] == oldSave[m].id)
                        {
                            GridManager.Instance.gridData[j, i] = newDatas[m].id;
                        }
                    }
                }
            }
            Debug.Log("gridSaveAndLoad.savedFiles[k]" + gridSaveAndLoad.savedFiles[k]);
            //si on est à la fin, on est sur [DataChange]tmp
            // (le dernier ajouté dans la liste, le supprimer
            /*if (k + 1 >= gridSaveAndLoad.savedFiles.Count)
            {
                Debug.Log("Delete last: " + gridSaveAndLoad.savedFiles[k]);
                gridSaveAndLoad.Delete(gridSaveAndLoad.savedFiles[k]);
            }                
            else
            {
                Debug.Log("save again: " + gridSaveAndLoad.savedFiles[k]);
                gridSaveAndLoad.Save(gridSaveAndLoad.savedFiles[k]);
            }*/
            Debug.Log("save again: " + gridSaveAndLoad.savedFiles[k]);
            gridSaveAndLoad.Save(gridSaveAndLoad.savedFiles[k]);
        }

        Debug.Log("ok, now laod, and delete tmp");
        gridSaveAndLoad.Load(tmpFile);
        gridSaveAndLoad.Delete(tmpFile);

        gridEditorUi.InitDropDown();
    }

    
}
