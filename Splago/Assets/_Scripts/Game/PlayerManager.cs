using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[TypeInfoBox("Manage an unique player")]
public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private float time = 30f;

    [ReadOnly]
    public SpellType spellType;
    [ReadOnly]
    public int levelSpell = 2;

    [SerializeField, ReadOnly]
    private int index;
    public int GetIndex() { return (index); }
    //private ushort realIndex;
    //public ushort GetRealIndex() { return (realIndex); }
    [SerializeField, ReadOnly]
    private string namePlayer;
    public string GetName() { return (namePlayer); }

    [SerializeField, ReadOnly]
    private Sprite spritePlayer;
    public Sprite GetSpritePlayer() { return (spritePlayer); }
    [SerializeField, ReadOnly]
    private Color colorSprite;
    public Color GetColorPlayer() { return (colorSprite); }

    [ShowInInspector, ReadOnly]
    private int timeToAdd = 0;

    private bool played = false;
    private Point lastClicked;
    private bool clickedOnce = false;       //click 2 times to validate !!

    public void Init(int _index, SpellType _spell, int _levelSpell)
    {
        index = _index;
        spellType = _spell;
        levelSpell = _levelSpell;

        //realIndex = GridDatas.Instance.GetRealIdPlayer(index);
        Debug.Log("init player data: " + index);
        namePlayer = ExtRandom.GetRandomName();

        if (GridDatas.Instance == null || GridDatas.Instance.cellsDatasPlayer.Count <= index)
            return;

        spritePlayer = GridDatas.Instance.cellsDatasPlayer[index].colorPlayer[0].sprite;
        colorSprite = GridDatas.Instance.cellsDatasPlayer[index].colorPlayer[0].color;
    }

    public float GetTime()
    {
        return (time + timeToAdd);
    }

    public void StartPlay()
    {
        Debug.Log("Active player " + GetName());
        played = false;
    }

    /// <summary>
    /// called by GameLoop only !
    /// only when a round is over
    /// </summary>
    /// <param name="timerLeft"></param>
    public void FinishRound(float timerLeft)
    {
        timeToAdd = (int)timerLeft / 2;

        if (timeToAdd > time / 2)
        {
            timeToAdd = (int)time / 2;
        }
        if (timeToAdd >= 1)
        {
            Debug.Log("added for next turn: " + timeToAdd);
        }
    }

    /// <summary>
    /// called when the player finish his turn
    /// </summary>
    public void EndPlay()
    {
        Debug.Log("time of player " + GetName() + " over");

        ValidateAtEndRound();

        played = true;
    }

    /// <summary>
    /// level = 0 = main
    /// 1 = hover
    /// 2 = poison...
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public ushort GetRealIndexNumber(int level)
    {
        return (GridDatas.Instance.cellsDatasPlayer[index].colorPlayer[level].id);
    }

    /// <summary>
    /// the player is over this case
    /// </summary>
    public void HoverCase(int x, int y, bool hover)
    {
        if (!hover)
        {
            //if we have clicked one time, don't reset !!
            if (clickedOnce)
                return;

            GridManager.Instance.ClearListLast();
            lastClicked = new Point(-1, -1);
            return;
        }
            
        ClickOnCase(x, y, false);
    }

    /// <summary>
    /// when time is running out, and we have clicked only once, reset
    /// </summary>
    private void ValidateAtEndRound()
    {
        if (!clickedOnce)
        {
            Debug.Log("ici si just hover ??");
            ResetClickOnce();
            return;
        }
            

        //simulate a click at the end of turn
        ClickOnCase(lastClicked.x, lastClicked.y, true, true);
    }

    /// <summary>
    /// when time is running out, and we have clicked only once, reset
    /// </summary>
    private void ResetClickOnce()
    {
        GridManager.Instance.ClearListLast();
        lastClicked = new Point(-1, -1);
        clickedOnce = false;
    }

    /// <summary>
    /// the player clic on this case
    /// </summary>
    public void ClickOnCase(int x, int y, bool fromClick = true, bool fromEndRound = false)
    {
        if (played && x == lastClicked.x && y == lastClicked.y)
        {
            if (!clickedOnce)
            {
                clickedOnce = true;
                ClickButDoHover(x, y);
                return;
            }

            

            Debug.Log("we click on the same !!!!");
            GridManager.Instance.ClearListLast();

            GridManager.Instance.SetSpells(this, spellType, levelSpell, x, y, 0);   //0 = definitif
            GridManager.Instance.ClearListLast(true);

            //finish round only if not already finished
            if (!fromEndRound)
                GameLoop.Instance.FinishRound();
            return;
        }

        if (!GridManager.Instance.IsSameTypeCellAndPlayer(x, y, GetRealIndexNumber(0)))
        {
            

            //if we have clicked one time, don't reset !!
            //only if we are on over
            if (clickedOnce && !fromClick)
                return;

            Debug.Log("can't click !! here reset last move");

            GridManager.Instance.ClearListLast();
            lastClicked = new Point(-1, -1);
            clickedOnce = false;
            return;
        }
        clickedOnce = false;

        ClickButDoHover(x, y);
    }

    /// <summary>
    /// here we are just hover a cells
    /// </summary>
    private void ClickButDoHover(int x, int y)
    {
        played = true;
        lastClicked = new Point(x, y);

        Debug.Log("[player " + index + "] click " + x + ", " + y);

        GridManager.Instance.ClearListLast();

        GridManager.Instance.SetSpells(this, spellType, levelSpell, x, y, 1);   //1 = hover
    }
}
