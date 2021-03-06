﻿using System.Collections;
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

[Serializable]
public struct CellsDatasPlayer
{
    public List<CellData> colorPlayer;
}

[TypeInfoBox("Contain data of all cells")]
public class GridDatas : SingletonMono<GridDatas>
{
    [Tooltip("toute les case utilisable sont ici !")]
	public List<CellData> cellsDatas = new List<CellData>();
    [Tooltip("toute les case utilisable sont ici !")]
    public List<CellsDatasPlayer> cellsDatasPlayer = new List<CellsDatasPlayer>();
    [Tooltip("toute les case utilisable sont ici !")]
    public List<CellData> cellsDatasBombes = new List<CellData>();
    [Tooltip("toute les case utilisable sont ici !")]
    public List<CellData> cellsDatasMisc = new List<CellData>();

    public string nameSpawn = "Spawn";
    public string nameWall = "Wall";

    [Tooltip("les cases non savable, mais utilisable par le client")]
    public List<CellData> cellVisualMisc = new List<CellData>();

    /// <summary>
    /// is this ID a spawn ?
    /// </summary>
    public ushort GetIdByName(string nameCell)
    {
        for (ushort i = 0; i < cellsDatas.Count; i++)
        {
            if (string.Equals(cellsDatas[i].name, nameCell))
            {
                return (cellsDatas[i].id);
            }
        }
        for (ushort i = 0; i < cellsDatasPlayer.Count; i++)
        {
            for (int j = 0; j < cellsDatasPlayer[i].colorPlayer.Count; j++)
            {
                if (string.Equals(cellsDatasPlayer[i].colorPlayer[j].name + cellsDatasPlayer[i].colorPlayer[j].id, nameCell))
                {
                    return (cellsDatasPlayer[i].colorPlayer[j].id);
                }
            }
        }
        for (ushort i = 0; i < cellsDatasBombes.Count; i++)
        {
            if (string.Equals(cellsDatasBombes[i].name, nameCell))
            {
                return (cellsDatasBombes[i].id);
            }
        }
        for (ushort i = 0; i < cellsDatasMisc.Count; i++)
        {
            if (string.Equals(cellsDatasMisc[i].name, nameCell))
            {
                return (cellsDatasMisc[i].id);
            }
        }
        for (ushort i = 0; i < cellVisualMisc.Count; i++)
        {
            if (string.Equals(cellVisualMisc[i].name, nameCell))
            {
                return (cellVisualMisc[i].id);
            }
        }
        Debug.LogWarning("NOT FINDED");
        return (0);
    }

    /// <summary>
    /// get all list packed in one
    /// </summary>
    /// <returns></returns>
    public List<CellData> GetackedData()
    {
        List<CellData> packed = new List<CellData>();
        for (ushort i = 0; i < cellsDatas.Count; i++)
            packed.Add(cellsDatas[i]);

        for (ushort i = 0; i < cellsDatasPlayer.Count; i++)
        {
            for (ushort j = 0; j < cellsDatasPlayer[i].colorPlayer.Count; j++)
                packed.Add(cellsDatasPlayer[i].colorPlayer[j]);
        }

        for (ushort i = 0; i < cellsDatasBombes.Count; i++)
            packed.Add(cellsDatasBombes[i]);
        for (ushort i = 0; i < cellsDatasMisc.Count; i++)
            packed.Add(cellsDatasMisc[i]);

        return (packed);
    }

    /// <summary>
    /// here change every id
    /// (Danger zone !)
    /// </summary>
    [Button]
    private void SetIdOfData()
    {
        //List<CellData> oldSave = new List<CellData>();
        List<CellData> newSave = new List<CellData>();
        ushort index = 0;
        for (ushort i = 0; i < cellsDatas.Count; i++)
        {
            //oldSave.Add(cellsDatas[i]);

            CellData cell = cellsDatas[i];
            cell.id = index;
            cellsDatas[i] = cell;

            newSave.Add(cell);
            index++;
        }

        for (ushort i = 0; i < cellsDatasPlayer.Count; i++)
        {
            for (ushort j = 0; j < cellsDatasPlayer[i].colorPlayer.Count; j++)
            {
                CellData cell = cellsDatasPlayer[i].colorPlayer[j];
                cell.id = index;
                cellsDatasPlayer[i].colorPlayer[j] = cell;

                newSave.Add(cell);
                index++;
            }
        }
        /*
        for (ushort i = 0; i < cellsDatasPlayer.Count; i++)
        {
            //oldSave.Add(cellsDatasPlayer[i]);

            CellData cell = cellsDatasPlayer[i];
            cell.id = index;
            cellsDatasPlayer[i] = cell;

            newSave.Add(cell);
            index++;
        }
        */
        for (ushort i = 0; i < cellsDatasBombes.Count; i++)
        {
            //oldSave.Add(cellsDatasBombes[i]);

            CellData cell = cellsDatasBombes[i];
            cell.id = index;
            cellsDatasBombes[i] = cell;

            newSave.Add(cell);
            index++;
        }
        for (ushort i = 0; i < cellsDatasMisc.Count; i++)
        {
            //oldSave.Add(cellsDatasMisc[i]);

            CellData cell = cellsDatasMisc[i];
            cell.id = index;
            cellsDatasMisc[i] = cell;

            newSave.Add(cell);
            index++;
        }

        //reset loaded map
        //GridEditor.Instance.DataGridChanged(oldSave, newSave);


        ////////////ici misc client
        //ushort index = 0;
        for (ushort i = 0; i < cellVisualMisc.Count; i++)
        {
            CellData cell = cellVisualMisc[i];
            cell.id = index;
            cellVisualMisc[i] = cell;
            index++;
        }
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

        for (int i = 0; i < cellsDatasPlayer.Count; i++)
        {
            for (int j = 0; j < cellsDatasPlayer[i].colorPlayer.Count; j++)
            {
                if (idUnique == cellsDatasPlayer[i].colorPlayer[j].id)
                    return (cellsDatasPlayer[i].colorPlayer[j]);
            }
        }

        for (int i = 0; i < cellsDatasBombes.Count; i++)
        {
            if (idUnique == cellsDatasBombes[i].id)
                return (cellsDatasBombes[i]);
        }
        for (int i = 0; i < cellsDatasMisc.Count; i++)
        {
            if (idUnique == cellsDatasMisc[i].id)
                return (cellsDatasMisc[i]);
        }
        for (int i = 0; i < cellVisualMisc.Count; i++)
        {
            if (idUnique == cellVisualMisc[i].id)
                return (cellVisualMisc[i]);
        }

        Debug.LogWarning("Cell not found !");
        return (cellsDatas[0]);
    }
}
