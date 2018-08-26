using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[TypeInfoBox("Manage teh gameloop of the game")]
public class GameUILink : MonoBehaviour
{
    [SerializeField]
    private List<TextMeshProUGUI> nameCurrentPlayerUI;      //manage both UI
    [SerializeField]
    private List<Image> currentImagePlayerUI;      //manage both UI
    [SerializeField]
    private List<TextMeshProUGUI> timerPlayerUI;      //manage both UI
    [SerializeField]
    private List<TextMeshProUGUI> turnGameUI;                  //manage both UI

    [SerializeField]
    private List<RectTransform> parentSlotNextUI;                  //manage both UI

    [SerializeField, ReadOnly]
    private int timerPlayer = 99;

    private GameLoop gameLoop;
    private PlayerManager currentPlayer;

    public void Init()
    {
        gameLoop = GameLoop.Instance;
        currentPlayer = null;
        NewGameTurn(true);
        InitNextSlotUI();
    }

    /// <summary>
    /// called when there are a new game Turn !
    /// </summary>
    public void NewGameTurn(bool first = false)
    {
        if (first)
        {
            //here the first turn
        }
        for (int i = 0; i < turnGameUI.Count; i++)
        {
            turnGameUI[i].text = gameLoop.GetCurrentTurn().ToString();
        }
    }


    /// <summary>
    /// init prefabs, next slot UI
    /// </summary>
    private void InitNextSlotUI()
    {
        for (int k = 0; k < parentSlotNextUI.Count; k++)
        {
            //loop thought player, from current, to MAX (may not be in Order playing
            for (int i = 0; i < GameLoop.Instance.GetOrderPlayer().Count - 1; i++)
            {
                parentSlotNextUI[k].GetChild(i).gameObject.SetActive(true);
            }
            for (int i = GameLoop.Instance.GetOrderPlayer().Count - 1; i < GameLoop.maxPlayer; i++)
            {
                parentSlotNextUI[k].GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// here a new round is ready, start !
    /// </summary>
    public void StartNewRound()
    {
        currentPlayer = gameLoop.GetCurrentPlayer();
        string nameCurrentPlayer = currentPlayer.GetName();
        for (int i = 0; i < nameCurrentPlayerUI.Count; i++)
        {
            nameCurrentPlayerUI[i].text = nameCurrentPlayer;
        }
        for (int i = 0; i < currentImagePlayerUI.Count; i++)
        {
            currentImagePlayerUI[i].sprite = currentPlayer.GetSpritePlayer();
            currentImagePlayerUI[i].color = currentPlayer.GetColorPlayer();
        }
        DisplayNextUI();
    }

    /// <summary>
    /// update Next Slot UI
    /// </summary>
    private void DisplayNextUI()
    {
        for (int k = 0; k < parentSlotNextUI.Count; k++)
        {
            int indexNext = currentPlayer.GetIndex() + 1;

            //loop thought player, from current, to MAX (may not be in Order playing
            for (int i = 0; i < parentSlotNextUI[k].childCount; i++)
            {
                if (!parentSlotNextUI[k].GetChild(i).gameObject.IsActive())
                    continue;
                //get next index
                
                if (indexNext >= gameLoop.GetOrderPlayer().Count)
                    indexNext = 0;

                PlayerManager playerNext = gameLoop.GetOrderPlayer()[indexNext];
                parentSlotNextUI[k].GetChild(i).gameObject.GetComponent<NextSlot>().Init(playerNext);

                indexNext++;
            }
        }
    }

    /// <summary>
    /// update timer
    /// </summary>
    private void TryToUpdateTimer()
    {
        //if nobody play, don't do
        if (!gameLoop.PlayerIsPlaying())
            return;

        //if timerPlayer is different thant the playerTime, update the text !
        if (timerPlayer != (int)gameLoop.GetTimerPlayer().GetTimer())
        {
            timerPlayer = (int)gameLoop.GetTimerPlayer().GetTimer();
            for (int i = 0; i < timerPlayerUI.Count; i++)
            {
                timerPlayerUI[i].text = timerPlayer.ToString();
            }
        }
        //and here, update the scale !!
    }

    private void Update()
    {
        TryToUpdateTimer();
    }
}
