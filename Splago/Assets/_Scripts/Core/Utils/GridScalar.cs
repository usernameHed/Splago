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

    [OnValueChanged("Init")]
    public int rows = 6;
    [OnValueChanged("Init")]
    public int cols = 7;
    [OnValueChanged("Init")]
    public float spacing = 10;

    [FoldoutGroup("World UI scale"), SerializeField]
    private RectTransform gridPanel;
    [FoldoutGroup("World UI scale"), SerializeField]
    private RectTransform worldPanelSize;

    [FoldoutGroup("World UI scale"), OnValueChanged("Init"), SerializeField]
    private int portraitXPercentage = 95;
    [FoldoutGroup("World UI scale"), OnValueChanged("Init"), SerializeField]
    private int portraitYPercentage = 75;
    [FoldoutGroup("World UI scale"), OnValueChanged("Init"), SerializeField]
    private int landscapeXPercentage = 60;
    [FoldoutGroup("World UI scale"), OnValueChanged("Init"), SerializeField]
    private int landscapeYPercentage = 95;

    Vector2 lastSize;
    
    [Button]
    public void Init()
    {
        Init(rows, cols);
    }

	public void Init (int x, int y)
    {
        cols = y;
        rows = x;

        SetupPanelGridSize();

        parent = GetComponent<RectTransform>();
        gridPadding = grid.padding;
        lastSize = Vector2.zero;

        //SetupGrid();
        SetupGridSize();
    }
	
    private void SetupPanelGridSize()
    {
        //change gridPanel according to...% !!!
        //gridPanel.sizeDelta = ;
        Vector2 size = worldPanelSize.sizeDelta;
        //Debug.Log("size canvas: " + size);

        if (UILandscape.Instance.screenOrientation == ScreenOrientation.Portrait || !Application.isPlaying)
        {
            size.x = size.x / 100f * portraitXPercentage;
            size.y = size.y / 100f * portraitYPercentage;
        }
        else
        {
            size.x = size.x / 100f * landscapeXPercentage;
            size.y = size.y / 100f * landscapeYPercentage;
        }

        gridPanel.sizeDelta = size;
        //Debug.Log("size panel: " + gridPanel.sizeDelta);
    }

    private void SetupGridSize()
    {
        float x = gridPanel.sizeDelta.x;    //size X screen (percentage depending on grid)
        float y = gridPanel.sizeDelta.y;    //size Y screen
        float xg = rows;                    //x grid
        float yg = cols;                    //y grid

        float space = 0;                    //space between cells
        float cellSize = 0;                 //cells size
        float xrg = 0;                      //size x of all cells + spacing in x;
        float yrg = 0;                      //size y of all cells + spacing in y;

        Debug.Log("x screen: " + x);
        Debug.Log("y screen: " + y);
        Debug.Log("x grid: " + rows);
        Debug.Log("y grid: " + cols);

        //cas 1
        if (y >= x && yg > xg)
        {
            Debug.Log("cas 1");
            space = y / 100f * 1f;
            grid.spacing = new Vector2(space, space);

            cellSize = (y - (space * (yg - 1))) / yg;
            grid.cellSize = new Vector2(cellSize, cellSize);

            xrg = (xg * cellSize) + (space * (xg - 1));
            yrg = y;

            grid.padding.left = (int) ((x / 2.0f) - (xrg / 2.0f));
            grid.padding.top = 0;
        }
        //cas 2
        else if (y >= x && yg <= xg)
        {
            Debug.Log("cas 2");
            space = x / 100 * 1f;
            grid.spacing = new Vector2(space, space);

            cellSize = (x - (space * (xg - 1))) / xg;
            grid.cellSize = new Vector2(cellSize, cellSize);

            xrg = x;
            yrg = (yg * cellSize) + (space * (yg - 1));

            grid.padding.left = 0;
            grid.padding.top = (int)((y / 2.0f) - (yrg / 2.0f));
        }
        /*else if (y >= x && yg == xg)
        {
            Debug.Log("cas [1-2 square]");
            space = y / 100f * 1f;
            grid.spacing = new Vector2(space, space);

            cellSize = (y - (space * (yg - 1))) / yg;
            grid.cellSize = new Vector2(cellSize, cellSize);

            xrg = (xg * cellSize) + (space * (xg - 1));
            yrg = y;

            grid.padding.left = (int)((x / 2.0f) - (xrg / 2.0f));
            grid.padding.top = 0;
        }*/
        //cas 3
        else if (y < x && yg > xg)
        {
            Debug.Log("cas 3");
            space = y / 100 * 1f;
            grid.spacing = new Vector2(space, space);

            cellSize = (y - (space * (yg - 1))) / yg;
            grid.cellSize = new Vector2(cellSize, cellSize);

            xrg = (xg * cellSize) + (space * (xg - 1));
            yrg = y;

            grid.padding.left = (int)((x / 2.0f) - (xrg / 2.0f));
            grid.padding.top = 0;
        }
        //cas 4
        else if (y < x && yg <= xg)
        {
            Debug.Log("cas 4");
            space = x / 100 * 1f;
            grid.spacing = new Vector2(space, space);

            cellSize = (x - (space * (xg - 1))) / xg;
            grid.cellSize = new Vector2(cellSize, cellSize);

            xrg = x;
            yrg = (yg * cellSize) + (space * (yg - 1));

            grid.padding.left = 0;
            grid.padding.top = (int)((y / 2.0f) - (yrg / 2.0f));
        }
        /*else if (y < x && yg == xg)
        {
            Debug.Log("cas [3-4 square]");
        }*/
        else
        {
            Debug.LogError("As you wish");
        }
        Debug.Log("space: " + space);
        Debug.Log("cellSize: " + cellSize);
        Debug.Log("xrg: " + xrg);
        Debug.Log("yrg: " + yrg);
    }


    private void SetupGrid ()
    {
        return;

        if (parent == null)
            parent = GetComponent<RectTransform>();
        if (lastSize == parent.rect.size)
        {
            return;
        }
        grid.spacing = new Vector2(spacing, spacing);

        if (cols <= rows)
        {
            var paddingX = gridPadding.left + gridPadding.right;
            var cellSize = Mathf.Round((parent.rect.width - paddingX - (rows - 1) * spacing) / rows);
            grid.cellSize = new Vector2(cellSize, cellSize);
        }
        else
        {
            var paddingY = gridPadding.top + gridPadding.bottom;
            var cellSize = Mathf.Round((parent.rect.height - paddingY - (cols - 1) * spacing) / cols);
            grid.cellSize = new Vector2(cellSize, cellSize);
        }
        
	}
}
