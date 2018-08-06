using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplagoManager : MonoBehaviour, ILevelManager
{
    public void InitScene()
    {
        
    }

    public void InputLevel()
    {
        
    }

    [Button("Restart")]
    public void Play()
    {
        GameManager.Instance.SceneManagerLocal.PlayNext();
    }
    [Button("Back")]
    public void Quit()
    {
        GameManager.Instance.SceneManagerLocal.PlayPrevious();
    }
}
