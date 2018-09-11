using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

[Serializable]
public struct SlotUi
{
    public List<RectTransform> nextPlayer;
    public RectTransform separator;
    public TextMeshProUGUI textNextTurn;
}

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
    private List<GameObject> timerPlayerFinishTurn;      //manage both UI

    [SerializeField]
    private List<TextMeshProUGUI> turnGameUI;                  //manage both UI
    [SerializeField]
    private List<RectTransform> redTimer;                  //manage both UI
    [SerializeField]
    private List<RectTransform> canvasSize;                  //manage both UI

    [SerializeField]
    private List<SlotUi> parentSlotNextUI;                  //manage both UI

    [SerializeField, ReadOnly]
    private int timerPlayer = 99;

    [SerializeField]
    private int hotTimer = 5;

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
        for (int k = 0; k < parentSlotNextUI.Count; k++)
        {
            parentSlotNextUI[k].textNextTurn.text = (gameLoop.GetCurrentTurn() + 1).ToString();
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
                parentSlotNextUI[k].nextPlayer[i].gameObject.SetActive(true);
            }
            for (int i = GameLoop.Instance.GetOrderPlayer().Count - 1; i < GameLoop.maxPlayer - 1; i++)
            {
                parentSlotNextUI[k].nextPlayer[i].gameObject.SetActive(false);
            }
            parentSlotNextUI[k].separator.gameObject.SetActive(true);
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
            parentSlotNextUI[k].separator.transform.SetSiblingIndex(gameLoop.GetOrderPlayer().Count);

            //loop thought player, from current, to MAX (may not be in Order playing
            for (int i = 0; i < parentSlotNextUI[k].nextPlayer.Count; i++)
            {
                if (!parentSlotNextUI[k].nextPlayer[i].gameObject.IsActive())
                    continue;
                //get next index
                
                if (indexNext >= gameLoop.GetOrderPlayer().Count)
                {
                    Debug.Log("separator: " + i + ", selected one: " + currentPlayer.GetIndex());
                    /*if (currentPlayer.GetIndex() == 0)
                        parentSlotNextUI[k].separator.transform.SetSiblingIndex(gameLoop.GetOrderPlayer().Count);
                    else*/
                        parentSlotNextUI[k].separator.transform.SetSiblingIndex(i);
                    indexNext = 0;
                }
                    

                PlayerManager playerNext = gameLoop.GetOrderPlayer()[indexNext];
                parentSlotNextUI[k].nextPlayer[i].gameObject.GetComponent<NextSlot>().Init(playerNext);

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
                if (timerPlayer <= hotTimer)
                {
                    timerPlayerUI[i].gameObject.SetActive(true);
                    timerPlayerFinishTurn[i].gameObject.SetActive(false);
                }
                else
                {
                    timerPlayerUI[i].gameObject.SetActive(false);
                    timerPlayerFinishTurn[i].gameObject.SetActive(true);
                }
            }
        }
        //and here, update the scale !!
        for (int i = 0; i < redTimer.Count; i++)
        {
            float actualPercentPlayer = gameLoop.GetTimerPlayer().GetTimer() * 100 / currentPlayer.GetTime();
            //Debug.Log("percent: " + actualPercentPlayer);

            //Debug.Log(canvasSize[i].sizeDelta);
            if (i == 0)
            {
                float xSizeRedTime = (actualPercentPlayer * canvasSize[i].sizeDelta.x / 100);
                redTimer[i].offsetMin = new Vector2(canvasSize[i].sizeDelta.x - xSizeRedTime, redTimer[i].sizeDelta.y);
            }
                
            else
            {
                float ySizeRedTime = (actualPercentPlayer * canvasSize[i].sizeDelta.y / 100);
                redTimer[i].offsetMax = new Vector2(redTimer[i].sizeDelta.x, ySizeRedTime - canvasSize[i].sizeDelta.y);
            }
                

            //currentPlayer.GetTime() = 100;

        }
    }

    private void Update()
    {
        TryToUpdateTimer();
    }
}
