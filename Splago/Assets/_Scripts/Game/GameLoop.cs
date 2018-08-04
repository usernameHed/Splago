using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : SingletonMono<GameLoop>
{
    public GameObject cursorPlayer;

    /// <summary>
    /// here init everything for the game start
    /// </summary>
    private void Start()
    {
        cursorPlayer.SetActive(false);
        GridManager.Instance.InitGrid();
    }

    /// <summary>
    /// the player is over this case
    /// </summary>
    public void OverCase(int x, int y, Transform parent)
    {
        Debug.Log("[game] over " + x + ", " + y);
        
        cursorPlayer.transform.SetParent(parent);
        cursorPlayer.GetComponent<RectTransform>().ResetPos();
        cursorPlayer.SetActive(true);
    }
    public void OverExitCase(int x, int y)
    {
        Debug.Log("[game] over " + x + ", " + y);
        cursorPlayer.SetActive(false);
        cursorPlayer.transform.SetParent(transform);
    }
    /// <summary>
    /// the player clic on this case
    /// </summary>
    public void ClickOnCase(int x, int y)
    {
        Debug.Log("[game] click " + x + ", " + y);
    }
}
