using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WidthDynamic : MonoBehaviour
{
    [SerializeField]
    private float percentageOfScreenX = 30f;
    [SerializeField]
    private bool useX = false;

    [SerializeField]
    private float percentageY = 0f;
    [SerializeField]
    private bool useY = false;


    [SerializeField]
    private RectTransform widthToAdjust;

    [SerializeField]
    private RectTransform referenceWidth;

    private void OnEnable()
    {
        EventManager.StartListening(GameData.Event.ResolutionChange, OnResolutionChange);
    }

    [Button]
    private void OnResolutionChange()
    {
        float xScreen = referenceWidth.sizeDelta.x;
        float yScreen = referenceWidth.sizeDelta.y;
        float percentagePanelX = (useX) ? percentageOfScreenX * xScreen / 100f : widthToAdjust.sizeDelta.x;
        float percentagePanelY = (useY) ? percentageY * yScreen / 100f : widthToAdjust.sizeDelta.y;
        widthToAdjust.sizeDelta = new Vector2(percentagePanelX, percentagePanelY);
    }

    private void OnDisable()
    {
        EventManager.StopListening(GameData.Event.ResolutionChange, OnResolutionChange);
    }
}
