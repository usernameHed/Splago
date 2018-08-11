using UnityEngine;
using UnityEngine.UI;   
using System.Collections;
using Sirenix.OdinInspector;

/// <summary>
/// Scale a GridLayoutGroup according to resolution, etc.
/// This is using width-constrained layout
/// </summary>
[TypeInfoBox("Scale a GridLayoutGroup according to resolution")]
public class GridScalar : MonoBehaviour
{
    [SerializeField]
    private GridLayoutGroup grid;

    private RectOffset gridPadding;
    private RectTransform parent;

    [OnValueChanged("Setup")]
    public int rows = 6;
    [OnValueChanged("Setup")]
    public int cols = 7;
    [OnValueChanged("Setup")]
    public float spacing = 10;

    Vector2 lastSize;
    
	public void Init (int x, int y)
    {
        cols = y;
        rows = x;

        
        parent = GetComponent<RectTransform>();
        gridPadding = grid.padding;
        lastSize = Vector2.zero;

        Setup();
    }
	
	// Update is called once per frame
	void Setup ()
    {
        if (parent == null)
            parent = GetComponent<RectTransform>();
        if (lastSize == parent.rect.size)
        {
            return;
        }
        grid.spacing = new Vector2(spacing, spacing);

        var paddingX = gridPadding.left + gridPadding.right;
        var cellSize = Mathf.Round((parent.rect.width - paddingX - (rows - 1) * spacing) / rows);
        grid.cellSize = new Vector2(cellSize, cellSize);
	}
}
