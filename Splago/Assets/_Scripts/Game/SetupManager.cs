using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[TypeInfoBox("[ILevelLocal] Manage Setup Scene behaviour")]
public class SetupManager : MonoBehaviour, ILevelLocal
{
    public void InitScene()
    {
        Debug.Log("INIT setup !!");
        SpawnManager.Instance.Init1v1();
    }
}
