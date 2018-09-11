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
    private ushort realIndex;
    public ushort GetRealIndex() { return (realIndex); }
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
    private int lastX = -1;
    private int lastY = -1;

    public void Init(int _index, SpellType _spell, int _levelSpell)
    {
        index = _index;
        spellType = _spell;
        levelSpell = _levelSpell;

        realIndex = GridDatas.Instance.GetRealIdPlayer(index);
        Debug.Log("init player data: " + index);
        namePlayer = ExtRandom.GetRandomName();

        if (GridDatas.Instance == null || GridDatas.Instance.cellsDatasPlayer.Count <= index)
            return;

        spritePlayer = GridDatas.Instance.cellsDatasPlayer[index].sprite;
        colorSprite = GridDatas.Instance.cellsDatasPlayer[index].color;
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

    public void EndPlay()
    {
        if (!played)
        {
            HoverCase(lastX, lastY, false);
        }
        Debug.Log("time of player " + GetName() + " over");
        played = true;
    }

    /// <summary>
    /// the player is over this case
    /// </summary>
    public void HoverCase(int x, int y, bool hover)
    {
        if (played)
            return;
        lastX = x;
        lastY = y;

        if (!GridManager.Instance.IsSameTypeCellAndPlayer(x, y, realIndex))
        {
            //Debug.Log("can't hover !!");
            return;
        }

        Debug.Log("[player " + index + "] over " + x + ", " + y);
        GridManager.Instance.SetSpellsHover(realIndex, spellType, levelSpell, x, y, hover);
    }

    /// <summary>
    /// the player clic on this case
    /// </summary>
    public void ClickOnCase(int x, int y)
    {
        if (!GridManager.Instance.IsSameTypeCellAndPlayer(x, y, realIndex))
        {
            Debug.Log("can't click !!");
            return;
        }

        played = true;
        Debug.Log("[player " + index + "] click " + x + ", " + y);

        GridManager.Instance.SetSpells(realIndex, spellType, levelSpell, x, y);
    }
}
