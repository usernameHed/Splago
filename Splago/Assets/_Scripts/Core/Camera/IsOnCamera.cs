using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Check object is on camera
/// <summary>
public class IsOnCamera : MonoBehaviour
{
    [SerializeField]
    private bool onEnableAdd = true;

    [SerializeField]
    private bool onDisableRemove = true;

    [SerializeField]
	private FrequencyTimer updateTimer = new FrequencyTimer(1.0F);

	[SerializeField]
	private float xMargin;

	[SerializeField]
	private float yMargin;

    [ReadOnly]
	public bool isOnScreen = false;
    [ReadOnly]
    public bool IsTooMuchInside = false;


    public float interneBorder = 0.2f;

    [ShowInInspector, ReadOnly]
    private Image objectRendererUI;
    [ShowInInspector, ReadOnly]
    private Renderer objectRenderer;

    [ShowInInspector, ReadOnly]
    private Camera cam;

    [SerializeField]
    private CameraSplago cameraController;

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
	private void CheckOnCamera()
	{
		if (!cam || (objectRendererUI == null && objectRenderer == null))
		{
            TryToGetCam();
            if (!cam || (objectRendererUI == null && objectRenderer == null))
                return;
		}

		Vector3 bottomCorner = cam.WorldToViewportPoint(gameObject.transform.position - bounds);
		Vector3 topCorner = cam.WorldToViewportPoint(gameObject.transform.position + bounds);

        
        isOnScreen = (topCorner.x >= -xMargin && bottomCorner.x <= 1 + xMargin && topCorner.y >= -yMargin && bottomCorner.y <= 1 + yMargin);
        IsTooMuchInside = (topCorner.x >= -interneBorder && bottomCorner.x <= 1 + interneBorder && topCorner.y >= -interneBorder && bottomCorner.y <= 1 + interneBorder);

    }


    public void AddTarget()
    {
        cameraController.AddTarget(this);
    }

    public void RemoveTarget()
    {
        if (cameraController)
        {
            cameraController.RemoveTarget(this);
        }
    }

    // Unity functions
    private void OnEnable()
    {
        if (onEnableAdd)
        {
            AddTarget();
        }
    }

    private void OnDisable()
    {
        if (onDisableRemove)
        {
            RemoveTarget();
        }
    }

    private void OnDestroy()
    {
        RemoveTarget();
    }

    // Unity functions

    private void Update()
    {
		if (updateTimer.Ready())
        {
			CheckOnCamera();
        }
    }
	#endregion
}
