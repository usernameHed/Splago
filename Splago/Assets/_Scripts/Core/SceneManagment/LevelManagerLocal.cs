using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerLocal : MonoBehaviour, ILevelManager
{
    public GameObject canvasPortrait;
    public GameObject canvasLandscape;
    public GameObject canvasWorld;
    public GameObject ILevelLocal;

    private ILevelLocal levelManger;

    public void InitScene()
    {
        if (ILevelLocal)
            levelManger = ILevelLocal.GetComponent<ILevelLocal>();
        if (levelManger != null)
            levelManger.InitScene();

        //Debug.Log("nothing for this scene");
        UILandscape.Instance.Init(canvasPortrait, canvasLandscape, canvasWorld);
    }

    public void InputLevel()
    {
        //Debug.Log("nothing for this scene");
    }

    public void Play()
    {
        //Debug.Log("play next");
        GameManager.Instance.SceneManagerLocal.PlayNext();
    }

    public void Previous()
    {
        GameManager.Instance.SceneManagerLocal.PlayPrevious();
    }

    public void PlayIndex(int index)
    {
        //Debug.Log("nothing for this scene");
    }
}
