﻿using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System;
using Sirenix.Serialization;

/// <summary>
/// GameManager Description
/// </summary>
public class GameManager : SingletonMono<GameManager>
{
    #region Attributes
    [FoldoutGroup("Debug"), Tooltip("opti fps"), SerializeField]
	private FrequencyTimer updateTimer;

    [FoldoutGroup("Debug"), Tooltip("Caméra"), SerializeField]
    private GameObject cameraObject;
    public GameObject CameraObject
    {
        get
        {
            if (!cameraObject)
            {
                cameraMain = Camera.main;
                cameraObject = cameraMain.gameObject;
                return (cameraObject);
            }
            return cameraObject;
        }
    }
    public void SetCamera(GameObject cam) { cameraMain = (cam) ? cam.GetComponent<Camera>() : null; }
    private Camera cameraMain;
    public Camera CameraMain { get { return (cameraMain); } }


    [FoldoutGroup("Scenes"), Tooltip("liens du levelManager"), SerializeField]
    private SceneManagerLocal sceneManagerLocal;
    public SceneManagerLocal SceneManagerLocal { set { sceneManagerLocal = value; InitNewScene(); } get { return (sceneManagerLocal); } }

    #endregion

    #region Initialization

    private void OnEnable()
    {

    }

    /// <summary>
    /// initialise les ILevelManagers (il y en a forcément 1 par niveau)
    /// </summary>
    private void InitNewScene()
    {
        SetCamera(cameraObject);
    }

    #endregion

    #region Core
    
    #endregion

    #region Unity ending functions

    private void Update()
    {
        //optimisation des fps
        if (updateTimer.Ready())
        {
            
        }
    }

    private void OnDisable()
    {

    }
    #endregion
}
