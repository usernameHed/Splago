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

    private Vector2 size = new Vector2();
    private float x = 0;                        //size X screen (percentage depending on grid)
    private float y = 0;                        //size Y screen
    private float xg = 0;                       //x grid
    private float yg = 0;                       //y grid

    private float space = 0;                    //space between cells
    private float cellSize = 0;                 //cells size
    private float xrg = 0;                      //size x of all cells + spacing in x;
    private float yrg = 0;                      //size y of all cells + spacing in y;

    private int paddingLeft = 0;
    private int paddingTop = 0;

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
        SetupGridSize();
    }
	
    private void SetupPanelGridSize()
    {
        size = worldPanelSize.sizeDelta;
        Debug.Log("size worldPanel: " + size);

        if (UILandscape.Instance.screenOrientation == ScreenOrientation.Portrait || !Application.isPlaying)
        {
            size.x = size.x / 100 * portraitXPercentage;
            size.y = size.y / 100 * portraitYPercentage;
        }
        else
        {
            size.x = size.x / 100 * landscapeXPercentage;
            size.y = size.y / 100 * landscapeYPercentage;
        }

        Debug.Log("size gridPanel after %age: " + size);

        gridPanel.sizeDelta = size;
    }

    private bool Case1()
    {
        space = y / 100 * 1;                                //set space
        cellSize = (y - (space * (yg - 1))) / yg;           //set cell size
        xrg = (xg * cellSize) + (space * (xg - 1));         //set x size of all cellSize + space
        yrg = y;                                            //idem for y (normally it take all the y !)
        paddingLeft = (int)((x / 2.0f) - (xrg / 2.0f));     //calculate padding left
        paddingTop = 0;                                     //0 to top

        Debug.Log("xrg: " + xrg + ", and x: " + x);
        if (xrg > x)
        {
            Debug.Log("do inverse !!");
            return (false);
        }
        return (true);
    }
    private bool Case2()
    {
        space = x / 100 * 1;                                //set space
        cellSize = (x - (space * (xg - 1))) / xg;           //set cell size
        xrg = x;                                            //set x size of all cellSize + space
        yrg = (yg * cellSize) + (space * (yg - 1));         //idem for y (normally it take all the y !)
        paddingLeft = 0;                                    //calculate padding left
        paddingTop = (int)((y / 2.0f) - (yrg / 2.0f));      //0 to top

        Debug.Log("yrg: " + yrg + ", and y: " + y);
        if (yrg > y)
        {
            Debug.Log("do inverse !!");
            return (false);
        }
        return (true);
    }

    private void SetupGridSize()
    {
        x = size.x;    //size X screen (percentage depending on grid)
        y = size.y;    //size Y screen (percentage depending on grid)
        xg = rows;     //x grid
        yg = cols;     //y grid
        space = 0;
        cellSize = 0;
        xrg = 0;
        yrg = 0;

        Debug.Log("size x,y after %: " + x + ", " + y);

        //cas 1
        if (y >= x && yg > xg)
        {
            Debug.Log("cas 1");
            if (!Case1())
            {
                Debug.Log("ok, calcule case 2 !!!");
                Case2();
            }

        }
        //cas 2
        else if ( (y >= x && yg <= xg))
        {
            if (!Case2())
            {
                Debug.Log("ok, calcule case 2 !!!");
                Case1();
            }
        }
        //cas 3
        else if (y < x && yg > xg)
        {
            if (!Case1())
            {
                Debug.Log("ok, calcule case 2 !!!");
                Case2();
            }
        }
        //cas 4
        else if (y < x && yg <= xg)
        {
            Debug.Log("cas 4");
            if (!Case2())
            {
                Debug.Log("ok, calcule case 2 !!!");
                Case1();
            }
        }
        else
        {
            Debug.LogError("As you wish");
        }
        grid.spacing = new Vector2(space, space);
        grid.cellSize = new Vector2(cellSize, cellSize);
        grid.padding.left = paddingLeft;
        grid.padding.top = paddingTop;
    }
}
