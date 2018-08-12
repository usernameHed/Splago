﻿using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILandscape : SingletonMono<UILandscape>
{
    [SerializeField]
    private GameObject parentGameCanvas;

    [ShowInInspector, ReadOnly]
    private ScreenOrientation screenOrientation;

    [FoldoutGroup("Filled by ILevelManager"), ReadOnly]
    public GameObject canvasWorld;
    [FoldoutGroup("Filled by ILevelManager"), ReadOnly]
    public GameObject portrait;
    [FoldoutGroup("Filled by ILevelManager"), ReadOnly]
    public GameObject landscape;

    private RectTransform rtUiWorld;

    public void Init(GameObject UIPortrait, GameObject UILand, GameObject UIWorld)
    {
        parentGameCanvas.transform.ClearChild();

        portrait = UIPortrait;
        portrait.transform.SetParent(parentGameCanvas.transform);

        landscape = UILand;
        landscape.transform.SetParent(parentGameCanvas.transform);

        canvasWorld = UIWorld;

        rtUiWorld = canvasWorld.GetComponent(typeof(RectTransform)) as RectTransform;

        screenOrientation = Screen.orientation;

        ChangeOrientation();
    }

    public void OnOrientationChange()
    {
        ChangeOrientation();
    }

    public void OnResolutionChange()
    {
        ChangeOrientation();
    }

    
    private void ChangeOrientation()
    {
        if (!portrait || !landscape)
            return;

        if (Screen.width > Screen.height)
            screenOrientation = ScreenOrientation.Landscape;
        else
            screenOrientation = ScreenOrientation.Portrait;

        rtUiWorld.sizeDelta = new Vector2(Screen.width, Screen.height);
        GameManager.Instance.CameraMain.orthographicSize = Screen.height / (2 * 1 );

        if (screenOrientation == ScreenOrientation.Portrait)
        {
            Debug.Log("portrait !");
            portrait.SetActive(true);
            landscape.SetActive(false);
        }
        else
        {
            Debug.Log("Landscape !");
            portrait.SetActive(false);
            landscape.SetActive(true);
        }
    }
}
