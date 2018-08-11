using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;

/// <summary>
/// Manage camera
/// Smoothly move camera to m_DesiredPosition
/// m_DesiredPosition is the barycenter of target list
/// </summary>
public class CameraSplago : MonoBehaviour
{
    #region Attributes
    public GameObject worldCanvasPos;

    public Vector3 setAverageTmp = Vector3.zero;
    public float speedDezoom = 1;

    private bool isZooming = false;
    private bool isDezooming = false;

    // Time before next camera move
    [FoldoutGroup("GamePlay"), Tooltip("Le smooth de la caméra"), SerializeField]
    private float smoothTime = 0.2f;
    private float tmpSmoothTime;
    [FoldoutGroup("GamePlay"), Tooltip("Le smooth de la caméra"), SerializeField]
    private float smoothLittleTime = 0.2f;
    [FoldoutGroup("GamePlay"), Tooltip("Le smooth de la caméra"), SerializeField]
    private float speedLerpSmooth = 1f;

    private bool isAlways = false;
    public void SetAlways(bool always)
    {
        if (always)
        {
            Debug.Log("little");
            
            isAlways = true;
            smoothTime = smoothLittleTime;
        }
        else
        {
            Debug.Log("revenir à la normal");
            //smoothTime = tmpSmoothTime;
            isAlways = false;
        }
    }
    private void LerpSmooth()
    {
        if (!isAlways && smoothTime != tmpSmoothTime)
        {
            smoothTime = Mathf.Lerp(smoothTime, tmpSmoothTime, Time.deltaTime * speedLerpSmooth);
        }
    }

    // Target approximation threshold
    [FoldoutGroup("GamePlay"), Tooltip("Approximation: la caméra est-elle sur sa cible ?"), SerializeField]
    private float focusThreshold = 0f;

    //Target list
    [FoldoutGroup("Debug"), Tooltip("list de target"), SerializeField]
    private List<IsOnCamera> targetList = new List<IsOnCamera>();
    [SerializeField]
	private FrequencyTimer updateTimer;

	private Vector3 currentVelocity;
	private bool freezeCamera = false;
	private Vector3 averageTargetPosition;
	public Vector3 TargetPosition
	{
		get { return averageTargetPosition; }
	}
    private float holdSmooth = 0;

    #endregion

    #region Init

    private void Start()
    {
        InitCamera();
    }

    private void InitCamera()
    {
        Vector3 posWorld = worldCanvasPos.transform.position;
        setAverageTmp = new Vector3(posWorld.x, posWorld.y, setAverageTmp.z);

        CancelInvoke("FallBack");
        if (holdSmooth == 0)
            holdSmooth = smoothTime;
        else
            smoothTime = holdSmooth;

        tmpSmoothTime = smoothTime;
    }
    #endregion

    #region Core

    /// <summary>
    /// Add target to camera
    /// </summary>
    public void AddTarget(IsOnCamera other)
    {
		// Check object is not already a target
        if (targetList.IndexOf(other) < 0)
        {
            freezeCamera = false;
            targetList.Add(other);
        }
    }

    /// <summary>
    /// Remove target from target list
    /// </summary>
	public void RemoveTarget(IsOnCamera other)
    {
        for (int i = 0; i < targetList.Count; i++)
        {
            if (targetList[i].GetInstanceID() == other.GetInstanceID())
            {
                targetList.RemoveAt(i);
                return;
            }
        }
    }

    /// <summary>
    /// clean la list des targets
    /// </summary>
    private void CleanListTarget()
    {
        for (int i = 0; i < targetList.Count; i++)
        {
            if (!targetList[i])
                targetList.RemoveAt(i);
        }
        SetFreez();
    }

    /// <summary>
    /// Clear targets
    /// </summary>
    public void ClearTarget()
    {
        targetList.Clear();
    }

    /// <summary>
    /// Calculate new camera position
    /// </summary>
    private void FindAveragePosition()
    {
        bool dezoom = false;
        for (int i = 0; i < targetList.Count; i++)
        {
            if (!targetList[i].isOnScreen)
            {
                dezoom = true;
                break;
            }
        }
        bool tooInside = true;
        for (int i = 0; i < targetList.Count; i++)
        {
            if (!targetList[i].IsTooMuchInside)
            {
                tooInside = false;
                break;
            }
        }

        if (dezoom && !tooInside)
            setAverageTmp.z -= speedDezoom;
        else if (tooInside)
            setAverageTmp.z += speedDezoom;

        averageTargetPosition = setAverageTmp;
    }

    /// <summary>
    /// Initialize camera
    /// </summary>
    public void InitializeCamera()
    {
        FindAveragePosition();
		transform.position = averageTargetPosition;
    }

    /// <summary>
    /// Check camera is placed on target position
    /// </summary>
    /// <returns></returns>
    private bool HasReachedTargetPosition()
    {
		float x = transform.position.x;
		float y = transform.position.y;
       
		return x > averageTargetPosition.x - focusThreshold && x < averageTargetPosition.x + focusThreshold && y > averageTargetPosition.y - focusThreshold && y < averageTargetPosition.y + focusThreshold;
    }

    /// <summary>
    /// setup le freez de la camera...
    /// </summary>
    private void SetFreez()
    {
        freezeCamera = (targetList.Count == 0);
    }

    #endregion

    #region unity ending
    private void Update()
    {
        SetFreez();
        LerpSmooth();

        if (updateTimer.Ready())
        {
            CleanListTarget();

            if (freezeCamera)
			{
				return;
			}

            FindAveragePosition();
        }
    }

    /// <summary>
    /// Smoothly move camera toward averageTargetPosition
    /// </summary>
    private void LateUpdate()
    {
        if (freezeCamera || HasReachedTargetPosition())
        {
            return;
        }

        // Move to desired position
        transform.position = Vector3.SmoothDamp(transform.position, averageTargetPosition, ref currentVelocity, smoothTime);
        //posLisener = transform.position;    //change listenerPosition
    }
    #endregion
}