using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

/// <summary>
/// structure comportant les information d'une case donné
/// </summary>
[Serializable]
public struct CellData
{
    [Tooltip("le nom (ex: destructible Wall - niveau 0)")]
    public string name;
    [Tooltip("l'id unique dela case"), ReadOnly]
    public ushort id;
    [Tooltip("la référence UI de la case")]
    public Sprite sprite;
    [Tooltip("couleur du sprite")]
    public Color color;
}

/// <summary>
/// contient la liste de touts les cases utilisables
/// </summary>
public class GridDatas : SingletonMono<GridDatas>
{
    [Tooltip("toute les case utilisable sont ici !")]
	public List<CellData> cellsDatas = new List<CellData>();

    [Tooltip("les cases non savable, mais utilisable par le client")]
    public List<CellData> cellVisualMisc = new List<CellData>();

    /// <summary>
    /// here change every id
    /// (Danger zone !)
    /// </summary>
    [Button]
    private void SetIdOfData()
    {
        List<CellData> oldSave = new List<CellData>(cellsDatas);
        ushort index = 0;
        for (ushort i = 0; i < cellsDatas.Count; i++)
        {
            CellData cell = cellsDatas[i];
            cell.id = index;
            cellsDatas[i] = cell;
            index++;
        }
        //ushort index = 0;
        for (ushort i = 0; i < cellVisualMisc.Count; i++)
        {
            CellData cell = cellVisualMisc[i];
            cell.id = index;
            cellVisualMisc[i] = cell;
            index++;
        }
        //reset loaded map
        GridEditor.Instance.DataGridChanged(oldSave, cellsDatas);
    }

    /// <summary>
    /// get a cellData, with ush info
    /// </summary>
    /// <param name="idUnique">id to find</param>
    /// <returns></returns>
    public CellData GetCellsByData(ushort idUnique)
    {
        for (int i = 0; i < cellsDatas.Count; i++)
        {
            if (idUnique == cellsDatas[i].id)
                return (cellsDatas[i]);
        }
        Debug.LogWarning("Cell not found !");
        return (cellsDatas[0]);
    }
}
