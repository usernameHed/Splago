using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : SingletonMono<GameLoop>
{
    //GridManager.Ins
    public void Start()
    {
        GridManager.Instance.InitGrid();
    }
}
