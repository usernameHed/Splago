using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasScreenChange : MonoBehaviour
{
    private ScreenOrientation screenOrientation;
    private Vector2 resolution; // Current Resolution

    private void OnRectTransformDimensionsChange()
    {
        // Check for an Orientation Change
        ScreenOrientation curOri = Screen.orientation;
        switch (curOri)
        {
            case ScreenOrientation.Unknown: // Ignore
                {
                    break;
                }
            default:
                {
                    if (screenOrientation != curOri)
                    {
                        screenOrientation = curOri;
                        UILandscape.Instance.OnOrientationChange();
                    }
                    break;
                }
        }

        // Check for a Resolution Change
        if ((resolution.x != Screen.width && resolution.x != Screen.height) || (resolution.y != Screen.height && resolution.y != Screen.width))
        {
            resolution = new Vector2(Screen.width, Screen.height);
            UILandscape.Instance.OnResolutionChange();
        }
    }
}
