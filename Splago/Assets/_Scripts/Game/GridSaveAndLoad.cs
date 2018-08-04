using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSaveAndLoad : MonoBehaviour
{
    public string pathMaps = "";
    public List<string> savedFiles = new List<string>();

    [SerializeField]
    private GridEditorUI gridEditorUi;

    private void Start()
    {
        Init();
    }

    /// <summary>
    /// init the savedFiles List
    /// </summary>
    [Button]
    private void Init()
    {
        Debug.Log("Init savedFiles");
        savedFiles.Add("map0");
        savedFiles.Add("map1");

        gridEditorUi.InitSavedMapDropDown(savedFiles);
    }

    /// <summary>
    /// get the name of the file, by index
    /// </summary>
    /// <param name="index"></param>
    public string GetSavedFileName(int index)
    {
        if (index >= savedFiles.Count)
            return ("");
        return (savedFiles[index]);
    }

    public void Save() { Save(gridEditorUi.saveInput); }
    public void Save(string nameFile)
    {
        Debug.Log("save");
        savedFiles.Add(nameFile);


        gridEditorUi.InitSavedMapDropDown(savedFiles);
    }

    public void Load() { Load(gridEditorUi.fileToLoad); }
    public void Load(string nameFile)
    {
        Debug.Log("Load " + nameFile);
    }

    public void Delete() { Delete(gridEditorUi.fileToDelete); }
    public void Delete(string nameFile)
    {
        Debug.Log("delete");
        savedFiles.Remove(nameFile);

        gridEditorUi.InitSavedMapDropDown(savedFiles);
    }

}
