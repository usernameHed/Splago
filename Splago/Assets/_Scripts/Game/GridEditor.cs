using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridEditor : SingletonMono<GridEditor>
{
    [Tooltip("active l'édition"), OnValueChanged("SetEditorMode")]
    public bool activeEdition = false;

    private void SetEditorMode()
    {
        if (activeEdition)
            Debug.Log("editor mode");
        else
            Debug.Log("no more editor mode");
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
    }
}
