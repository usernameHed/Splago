using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WidthDynamic : MonoBehaviour
{
    [SerializeField]
    private float percentageOfScreen = 30f;

    [SerializeField]
    private RectTransform widthToAdjust;

    private void OnEnable()
    {
        EventManager.StartListening(GameData.Event.ResolutionChange, OnResolutionChange);
    }

    [Button]
    private void OnResolutionChange()
    {
        float xScreen = Screen.width;
        float percentagePanel = percentageOfScreen * xScreen / 100f;
        widthToAdjust.sizeDelta = new Vector2(percentagePanel, widthToAdjust.sizeDelta.y);
    }

    private void OnDisable()
    {
        EventManager.StopListening(GameData.Event.ResolutionChange, OnResolutionChange);
    }
}
