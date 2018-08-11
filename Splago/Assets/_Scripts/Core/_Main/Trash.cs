using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
/// Trash Description
/// </summary>
[TypeInfoBox("Destroy the gameObject at start")]
public class Trash : MonoBehaviour
{
    private void Awake()
    {
        Destroy(gameObject);
    }
}
