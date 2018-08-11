using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
/// DontDestroyOnLoad Description
/// </summary>
[TypeInfoBox("Special type of Singleton who delete other DontDestroyOnLoad")]
public class DontDestroyOnLoad : MonoBehaviour
{
    //protected DontDestroyOnLoad() { } // guarantee this will be always a singleton only - can't use the constructor!
    [SerializeField]
    private Transform manager;
    [Tooltip("parentMisc")]
    public Transform misc;

    #region Attributes
    private static DontDestroyOnLoad instance;
    public static DontDestroyOnLoad GetSingleton
    {
        get { return instance; }
    }
    #endregion

    #region Initialization
    public void SetSingleton()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }            
        else if (instance != this)
            DestroyImmediate(gameObject);
    }

    private void Awake()
    {
        SetSingleton();
    }
    #endregion
}
