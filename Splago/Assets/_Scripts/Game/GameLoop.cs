using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[TypeInfoBox("Manage teh gameloop of the game")]
public class GameLoop : SingletonMono<GameLoop>
{
    [SerializeField, ReadOnly]
    private bool gameStart = false;
    [SerializeField, ReadOnly]
    private int idStart = -1;
    [SerializeField, ReadOnly]
    private PlayerManager currentPlayingPlayer = null;

    [FoldoutGroup("Misc"), Tooltip("cursor in grid")]
    public CursorGrid cursor;
    [FoldoutGroup("Misc"), Tooltip("cursor in grid"), OnValueChanged("ActiveEditorMode")]
    public bool editorMod = false;

    [SerializeField]
    private List<PlayerManager> orderPlayer;

    private FrequencyCoolDown timerPlayer = new FrequencyCoolDown();

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

        gameStart = false;
        idStart = -1;
        currentPlayingPlayer = null;

        InitMisc();                             //init cursor
        GridManager.Instance.InitGrid();        //init grid
        orderPlayer = SpawnManager.Instance.Spawn();          //spawn les players déja init

        if (orderPlayer.Count == 0)
        {
            Debug.LogError("no player !");
            return;
        }
        StartNewRound();
    }

    /// <summary>
    /// set current player (according the the order)
    /// </summary>
    private void SetCurrentPlayer()
    {
        idStart++;
        if (idStart >= orderPlayer.Count)
            idStart = 0;

        currentPlayingPlayer = orderPlayer[idStart];
    }

    private void StartNewRound()
    {
        SetCurrentPlayer();

        Debug.Log("Active player " + currentPlayingPlayer.GetName());
        timerPlayer.StartCoolDown(currentPlayingPlayer.GetTime());
    }

    private void TestEndRound()
    {
        if (timerPlayer.IsStartedAndOver())
        {
            Debug.Log("time of player " + currentPlayingPlayer.GetName() + " over");
            StartNewRound();
        }
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

    private void Update()
    {
        TestEndRound();
    }
}
