using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[TypeInfoBox("Manage teh gameloop of the game")]
public class GameLoop : SingletonMono<GameLoop>
{
    [ShowInInspector, ReadOnly]
    public static int maxPlayer = 4;

    [SerializeField, ReadOnly]
    private int currentGameTurn = 1;
    public int GetCurrentTurn() { return (currentGameTurn); }

    [SerializeField, ReadOnly]
    private bool playerIsPlaying = false;
    public bool PlayerIsPlaying() { return (playerIsPlaying); }
    [SerializeField, ReadOnly]
    private int idStart = -1;
    [SerializeField, ReadOnly]
    private PlayerManager currentPlayingPlayer = null;
    public PlayerManager GetCurrentPlayer() { return (currentPlayingPlayer); }

    [SerializeField]
    private List<PlayerManager> orderPlayer;
    public List<PlayerManager> GetOrderPlayer() { return (orderPlayer); }

    
    public GameUILink gameUILink;

    [FoldoutGroup("Misc"), Tooltip("cursor in grid")]
    public CursorGrid cursor;
    [FoldoutGroup("Misc"), Tooltip("cursor in grid"), OnValueChanged("ActiveEditorMode")]
    public bool editorMod = false;

    private FrequencyCoolDown timerPlayer = new FrequencyCoolDown();
    public FrequencyCoolDown GetTimerPlayer() { return (timerPlayer); }

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

        playerIsPlaying = false;
        idStart = -1;
        currentGameTurn = 1;
        currentPlayingPlayer = null;
        

        InitMisc();                             //init cursor
        GridManager.Instance.InitGrid();        //init grid
        orderPlayer = SpawnManager.Instance.Spawn();          //spawn les players déja init

        if (orderPlayer.Count == 0)
        {
            Debug.LogError("no player !");
            return;
        }
        gameUILink.Init();
        GridManager.Instance.SpawnInit();
        StartNewRound();        
    }

    

    /// <summary>
    /// set current player (according the the order)
    /// </summary>
    private void SetCurrentPlayer()
    {
        idStart++;
        if (idStart >= orderPlayer.Count)
        {
            currentGameTurn++;             //add new round if old one is finished !
            gameUILink.NewGameTurn();
            idStart = 0;
        }


        currentPlayingPlayer = orderPlayer[idStart];
    }

    /// <summary>
    /// setup a player and play the round !
    /// </summary>
    private void StartNewRound()
    {
        
        SetCurrentPlayer();

        //Debug.Log("Active player " + currentPlayingPlayer.GetName());
        timerPlayer.StartCoolDown(currentPlayingPlayer.GetTime());

        playerIsPlaying = true;

        currentPlayingPlayer.StartPlay();

        gameUILink.StartNewRound(); //init UI
    }

    /// <summary>
    /// clicked by button
    /// </summary>
    public void FinishRound()
    {
        currentPlayingPlayer.FinishRound(GetTimerPlayer().GetTimer());
        EndRound();
    }

    private void TestEndRound()
    {
        if (timerPlayer.IsStartedAndOver())
        {
            currentPlayingPlayer.FinishRound(0);
            EndRound();
        }
    }

    private void EndRound()
    {
        playerIsPlaying = false;
        timerPlayer.Reset();

        currentPlayingPlayer.EndPlay();

        StartNewRound();
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
        
        cursor.OverCase(cell, hover);
        currentPlayingPlayer.HoverCase(x, y, hover);
    }

    /// <summary>
    /// the player clic on this case
    /// </summary>
    public void ClickOnCase(int x, int y)
    {
        currentPlayingPlayer.ClickOnCase(x, y);
    }

    private void Update()
    {
        TestEndRound();
    }
}
