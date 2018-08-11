using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILandscape : SingletonMono<UILandscape>
{
    [SerializeField, OnValueChanged("ChangeOrientation")]
    private ScreenOrientation screenOrientation = ScreenOrientation.AutoRotation;

    public GameObject canvasWorld;
    public GameObject portrait;
    public GameObject landscape;
    public bool forceMode = false;

    private RectTransform rtUiWorld;

    public void Init(GameObject UIPortrait, GameObject UILand, GameObject UIWorld)
    {
        portrait = UIPortrait;
        landscape = UILand;
        canvasWorld = UIWorld;

        rtUiWorld = canvasWorld.GetComponent(typeof(RectTransform)) as RectTransform;
    }

    private void ChangeOrientation()
    {
        if (!portrait || !landscape)
            return;

        rtUiWorld.sizeDelta = new Vector2(Screen.width, Screen.height);
        GameManager.Instance.CameraMain.orthographicSize = Screen.height / (2 * 1 /*pixel size*/);

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

    private void TestOrientationChange()
    {
        if (!GameManager.Instance.CameraObject || !portrait || !landscape)
            return;

        if ((Input.deviceOrientation == DeviceOrientation.Portrait
            || Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown
            || Input.deviceOrientation == DeviceOrientation.Unknown
            ) && screenOrientation != ScreenOrientation.Portrait)
        {
            screenOrientation = ScreenOrientation.Portrait;
            ChangeOrientation();
        }
        else if ((Input.deviceOrientation == DeviceOrientation.LandscapeLeft
            || Input.deviceOrientation == DeviceOrientation.LandscapeRight
            ) && screenOrientation != ScreenOrientation.Landscape)

        {
            screenOrientation = ScreenOrientation.Landscape;
            ChangeOrientation();
        }
    }


    private void Update()
    {
        if (!forceMode)
            TestOrientationChange();
    }
}
