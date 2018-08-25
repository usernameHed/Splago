using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour, ILevelManager
{
    public GameObject canvasPortrait;
    public GameObject canvasLandscape;
    public GameObject canvasWorld;

    public void InitScene()
    {
        Debug.Log("nothing for this scene");
        UILandscape.Instance.Init(canvasPortrait, canvasLandscape, canvasWorld);
    }

    public void InputLevel()
    {
        Debug.Log("nothing for this scene");
    }

    public void Play()
    {
        Debug.Log("play next");
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
