using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NextSlot : MonoBehaviour
{
    [SerializeField]
    private Image imagePlayer;
    [SerializeField]
    private TextMeshProUGUI namePlayer;

    public void Init(PlayerManager player)
    {
        imagePlayer.sprite = player.GetSpritePlayer();
        imagePlayer.color = player.GetColorPlayer();
        namePlayer.text = player.GetName(); 
    }
}
