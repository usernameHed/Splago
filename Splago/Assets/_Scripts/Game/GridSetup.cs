using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine.UI;

public class GridSetup : MonoBehaviour
{
    //grid specifics
    [SerializeField, OnValueChanged("InitCells")]
    private int rows;
    [SerializeField, OnValueChanged("InitCells")]
    private int cols;
    [SerializeField, OnValueChanged("InitCells")]
    private Vector2 gridSize;
    [SerializeField, OnValueChanged("InitCells")]
    private Vector2 gridOffset;

    [SerializeField, OnValueChanged("InitCells")]
    private float xSpace;
    [SerializeField, OnValueChanged("InitCells")]
    private float ySpace;

    public GameObject casePrefabs;

    //about cells
    [SerializeField, OnValueChanged("InitCells")]
    private Sprite cellSprite;


    private Vector2 cellSize;
    private Vector2 cellScale;

    void Start ()
    {
        InitCells(); //Initialize all cells
	}

    //[Button("refresh")]
    void InitCells()
    {
        transform.ClearChild();


    }
}
