using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Check object is on camera
/// <summary>
public class IsOnCamera : MonoBehaviour
{
	[SerializeField]
	private FrequencyTimer updateTimer = new FrequencyTimer(1.0F);

	[SerializeField]
	private float xMargin;

	[SerializeField]
	private float yMargin;

	public bool isOnScreen = false;

    [ShowInInspector, ReadOnly]
    private Image objectRendererUI;
    [ShowInInspector, ReadOnly]
    private Renderer objectRenderer;

    [ShowInInspector, ReadOnly]
    private Camera cam;

    private Vector3 bounds;

    #region Initialization
    private void Start()
	{
        TryToGetCam();
    }
	#endregion

    #region Core

    private void TryToGetCam()
    {
        cam = GameManager.Instance.CameraMain;
        objectRendererUI = GetComponent<Image>();
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer)
            bounds = objectRenderer.bounds.extents;
        if (objectRendererUI)
            bounds = objectRendererUI.sprite.bounds.extents;
    }

	/// <summary>
	/// Check object is on screen
	/// <summary>
	bool CheckOnCamera()
	{
		if (!cam || (objectRendererUI == null && objectRenderer == null))
		{
            TryToGetCam();
            if (!cam || (objectRendererUI == null && objectRenderer == null))
                return false;
		}

		Vector3 bottomCorner = cam.WorldToViewportPoint(gameObject.transform.position - bounds);
		Vector3 topCorner = cam.WorldToViewportPoint(gameObject.transform.position + bounds);
        //Debug.Log("checkBorder");
		return (topCorner.x >= -xMargin && bottomCorner.x <= 1 + xMargin && topCorner.y >= -yMargin && bottomCorner.y <= 1 + yMargin);
	}

	// Unity functions

    private void Update()
    {
		if (updateTimer.Ready())
        {
			isOnScreen = CheckOnCamera();
        }
    }
	#endregion
}
