using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[TypeInfoBox("[ILevelLocal] Manage Splago Scene behaviour")]
public class SplagoManager : MonoBehaviour, ILevelLocal
{
    public bool devMod = true;

    public void InitScene()
    {
        Debug.Log("Init splago scene");
        if (devMod)
            SpawnManager.Instance.Init1v1();

        GameLoop.Instance.Init();
    }
}
