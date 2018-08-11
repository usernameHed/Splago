using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPortraitOrLandscape : MonoBehaviour
{
    [SerializeField]
    private GameObject portrait;
    [SerializeField]
    private GameObject landscape;

    private void Awake()
    {
        OrientationChange();
    }

    private void OrientationChange()
    {
        if (Input.deviceOrientation == DeviceOrientation.Portrait
            || Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown
            || Input.deviceOrientation == DeviceOrientation.Unknown
            )
        {
            portrait.SetActive(true);
            landscape.SetActive(false);
        }
        else if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft
            || Input.deviceOrientation == DeviceOrientation.LandscapeRight)
        {
            portrait.SetActive(false);
            landscape.SetActive(true);
        }
    }


    private void Update()
    {
        OrientationChange();
    }
}
