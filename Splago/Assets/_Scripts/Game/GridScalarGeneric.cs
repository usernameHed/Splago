using UnityEngine;
using UnityEngine.UI;   
using System.Collections;
using Sirenix.OdinInspector;

/// <summary>
/// Scale a GridLayoutGroup according to resolution, etc.
/// This is using width-constrained layout
/// </summary>
[TypeInfoBox("Scale a GridLayoutGroup according to resolution")]
public class GridScalarGeneric : MonoBehaviour
{
    [SerializeField]
    private RectTransform parent;

    [SerializeField]
    private GridLayoutGroup grid;

    private void OnEnable()
    {
        EventManager.StartListening(GameData.Event.ResolutionChange, InitGrid);
    }
    
    [Button]
    public void InitGrid()
    {
        float width = parent.rect.width;
        Vector2 newSize = new Vector2(width / 2, width / 2);
        grid.cellSize = newSize;
    }

    private void OnDisable()
    {
        EventManager.StopListening(GameData.Event.ResolutionChange, InitGrid);
    }
}
