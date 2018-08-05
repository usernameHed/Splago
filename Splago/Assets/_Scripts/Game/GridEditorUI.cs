using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class GridEditorUI : MonoBehaviour
{
    [SerializeField]
    private GameObject UiDevMod;

    [Tooltip("toggle active editor"), SerializeField]
    private Toggle toggleEditor;
    public void ToggleChanged() { GridEditor.Instance.SetEdition(toggleEditor.isOn); }

    [Tooltip("dropdown change id"), SerializeField]
    private Dropdown dropDown;
    [Tooltip("dropdown save and load"), SerializeField]
    private Dropdown dropDownSaveAndLoad;
    [Tooltip("dropdown save and load"), SerializeField]
    private Dropdown dropDownSaveAndLoadDelete;

    public void ActiveDevMod(bool active) { UiDevMod.SetActive(active); }

    public string saveInput = "default";   public void SetSaveInput(string name) { saveInput = name; } public InputField inputSave;
    [ReadOnly]
    public string fileToLoad = "";
    [ReadOnly]
    public string fileToDelete = "";

    /// <summary>
    /// init the dropdown with the CellData list of GridsDatas
    /// </summary>
    public void InitDropDown()
    {
        ActiveDevMod(GridEditor.Instance.IsInEditor());
        dropDown.options.Clear();
        List<string> optionDropDown = new List<string>();

        List<CellData> cellDataToLoop = GridDatas.Instance.GetackedData();
        for (int i = 0; i < cellDataToLoop.Count; i++)
        {
            optionDropDown.Add(cellDataToLoop[i].name);
        }
        dropDown.AddOptions(optionDropDown);
    }
    /// <summary>
    /// init dropdown with saved map
    /// </summary>
    public void InitSavedMapDropDown(List<string> savedMaps)
    {
        dropDownSaveAndLoad.options.Clear();
        dropDownSaveAndLoadDelete.options.Clear();

        dropDownSaveAndLoad.AddOptions(savedMaps);
        dropDownSaveAndLoadDelete.AddOptions(savedMaps);

        fileToLoad = GridEditor.Instance.gridSaveAndLoad.GetSavedFileName(dropDownSaveAndLoad.value);
        fileToDelete = GridEditor.Instance.gridSaveAndLoad.GetSavedFileName(dropDownSaveAndLoadDelete.value);
    }
    /// <summary>
    /// caled when dropDown change
    /// </summary>
    public void DropDownValueChanged()
    {
        GridEditor.Instance.SetIdUsh(dropDown.value);
    }

    /// <summary>
    /// caled when dropDown change
    /// </summary>
    public void DropDownSaveAndLoadValueChanged()
    {
        fileToLoad = GridEditor.Instance.gridSaveAndLoad.GetSavedFileName(dropDownSaveAndLoad.value);
    }
    /// <summary>
    /// caled when dropDown change
    /// </summary>
    public void DropDownSaveAndLoadDeleteValueChanged()
    {
        fileToDelete = GridEditor.Instance.gridSaveAndLoad.GetSavedFileName(dropDownSaveAndLoadDelete.value);
    }
}
