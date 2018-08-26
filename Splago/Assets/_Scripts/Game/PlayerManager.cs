using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[TypeInfoBox("Manage an unique player")]
public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private float time = 30f;
    public float GetTime() { return (time); }

    [SerializeField, ReadOnly]
    private int index;
    public int GetIndex() { return (index); }
    [SerializeField, ReadOnly]
    private string namePlayer;
    public string GetName() { return (namePlayer); }

    [SerializeField, ReadOnly]
    private Sprite spritePlayer;
    public Sprite GetSpritePlayer() { return (spritePlayer); }
    [SerializeField, ReadOnly]
    private Color colorSprite;
    public Color GetColorPlayer() { return (colorSprite); }


    public void Init(int _index)
    {
        index = _index;
        Debug.Log("init player data: " + index);
        namePlayer = ExtRandom.GetRandomName();
        spritePlayer = GridDatas.Instance.cellsDatasPlayer[index].sprite;
        colorSprite = GridDatas.Instance.cellsDatasPlayer[index].color;
    }
}
