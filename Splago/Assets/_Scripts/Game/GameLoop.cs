using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[TypeInfoBox("Manage teh gameloop of the game")]
public class GameLoop : SingletonMono<GameLoop>
{
    [FoldoutGroup("Misc"), Tooltip("cursor in grid")]
    public CursorGrid cursor;
    [FoldoutGroup("Misc"), Tooltip("cursor in grid"), OnValueChanged("ActiveEditorMode")]
    public bool editorMod = false;

    [SerializeField]
    private List<PlayerData> orderPlayer;

    /// <summary>
    /// initialize from leaving editor
    /// </summary>
    public void InitMisc()
    {
        cursor.Init(CursorGrid.CursorEnum.HoverSimple);
    }

    public void Init()
    {
        ActiveEditorMode();

        InitMisc();                             //init cursor
        GridManager.Instance.InitGrid();        //init grid
        SpawnManager.Instance.Spawn();          //spawn les players déja init
    }

    private void ActiveEditorMode()
    {
        if (editorMod)
            GridEditor.Instance.Init();
    }



    /// <summary>
    /// the player is over this case
    /// </summary>
    public void HoverCase(int x, int y, CellsBehaviour cell, bool hover)
    {
        //Debug.Log("[game] over " + x + ", " + y);
        cursor.OverCase(cell, hover);
    }

    /// <summary>
    /// the player clic on this case
    /// </summary>
    public void ClickOnCase(int x, int y)
    {
        Debug.Log("[game] click " + x + ", " + y);
    }
}
