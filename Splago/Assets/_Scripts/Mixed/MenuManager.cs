using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour, ILevelManager
{
    public void InitScene()
    {
        
    }

    public void InputLevel()
    {
        
    }

    [Button]
    public void Play()
    {
        GameManager.Instance.SceneManagerLocal.PlayNext();
    }
    [Button]
    public void Quit()
    {
        SceneManagerGlobal.Instance.QuitGame(true);
    }
}
