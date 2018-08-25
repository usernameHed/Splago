using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[TypeInfoBox("[ILevelManager] Manage Splago Scene behaviour")]
public class SplagoManager : MonoBehaviour, ILevelManager
{
    public GameObject UIWorld;
    public GameObject canvasPortrait;
    public GameObject canvasLandscape;

    public void InitScene()
    {
        Debug.Log("Init splago scene");
        UILandscape.Instance.Init(canvasPortrait, canvasLandscape, UIWorld);
        GameLoop.Instance.Init();
    }

    public void InputLevel()
    {
        Debug.Log("nothing for this scene");
    }


    public void Play()
    {
        GameManager.Instance.SceneManagerLocal.PlayNext();
    }

    public void Previous()
    {
        GameManager.Instance.SceneManagerLocal.PlayPrevious();
    }


    public void PlayIndex(int index)
    {
        Debug.Log("nothing for this scene");
    }
}
