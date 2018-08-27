using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GridSaveAndLoad : MonoBehaviour
{
    public string editorPath = "Assets/Resources";
    public string pathMaps = "Maps";
    public string extentionFile = "txt";
    public List<string> savedFiles = new List<string>();

    [SerializeField]
    private GridEditorUI gridEditorUi;

    /// <summary>
    /// init the savedFiles List
    /// </summary>
    [Button]
    public void Init()
    {
        //Debug.Log("Init savedFiles");
        savedFiles.Clear();

        //ici parcourt le dossier Resousrce/pathMaps avec la methode de zameran
        var pathSavedMaps = $"{pathMaps}/";

        //Debug.Log(pathSavedMaps);
        var sprites = Resources.LoadAll(pathSavedMaps);
        //Debug.Log(sprites);
        //Debug.Log(sprites.Length);
        int indexMaps = 0;
        foreach (var sprite in sprites)
        {
            
            string nameSprite = sprite.name;
            //Debug.Log(nameSprite);

            savedFiles.Add(nameSprite);
            indexMaps++;
        }

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
        Debug.Log("save (not in runtime !)");
        if (string.IsNullOrEmpty(nameFile))
            return;

        //ici save les courantes data dans le ficher nameFile (créé ou remplace)
        var pathSavedMaps = $"{editorPath}/{pathMaps}/{nameFile}.{extentionFile}";

        int sizeX = GridManager.Instance.SizeX;
        int sizeY = GridManager.Instance.SizeY;
        string content = sizeX + " " + sizeY + "\n" + GridManager.Instance.GetAllDataToString();

        ExtFile.SaveFromEditor(pathSavedMaps, content);

        /*using (StreamWriter writer =
        new StreamWriter(pathSavedMaps))
        {
            //writer.Write("Word ");
            int sizeX = GridManager.Instance.SizeX;
            int sizeY = GridManager.Instance.SizeY;
            writer.WriteLine(sizeX + " " + sizeY);
            writer.WriteLine(GridManager.Instance.GetAllDataToString());
        }*/

        if (savedFiles.Contains(nameFile))
        {
            Debug.Log("witing into existing files");
        }
        else
            savedFiles.Add(nameFile);


        gridEditorUi.InitSavedMapDropDown(savedFiles);
    }

    public void Load() { Load(gridEditorUi.fileToLoad); }
    public bool Load(string nameFile)
    {
        Debug.Log("Load " + nameFile);
        if (string.IsNullOrEmpty(nameFile))
        {
            Debug.LogWarning("isNull");
            return (false);
        }
            

        var pathSavedMaps = $"{pathMaps}/{nameFile}";
        //ici load les donnée de nameFile dans les data courante
        //string dataToLoad = "";

        string content = "";
        Debug.Log("path map to load: " + pathSavedMaps);
        bool openFile = ExtFile.LoadFromResource(pathSavedMaps, ref content);


        if (!openFile)
        {
            Debug.LogWarning("file doesn't exist");
            return (false);
        }

        string[] contentLine = content.Split('\n');

        int sizeXData = 0;
        int sizeYData = 0;
        ushort[,] gridData = new ushort[0, 0];

        int index = 0;
        for (int k = 0; k < contentLine.Length; k++)
        {
            //Debug.Log(contentLine[k]);
            //dataToLoad += s + "\n";
            if (index == 0)
            {
                string[] sizes = contentLine[k].Split(' ');
                sizeXData = sizes[0].ToInt(0);
                sizeYData = sizes[1].ToInt(0);

                gridData = new ushort[sizeXData, sizeYData];
            }
            else
            {
                string[] lineDatas = contentLine[k].Split(' ');
                for (int i = 0; i < lineDatas.Length; i++)
                {
                    if (string.IsNullOrWhiteSpace(lineDatas[i]))
                        continue;
                    //Debug.Log(lineDatas[i]);
                    gridData[i, index - 1] = (ushort)(lineDatas[i].ToInt(0));
                    //Debug.Log("done");
                }
            }

            index++;
        }
        
        GridManager.Instance.LoadNewGrid(sizeXData, sizeYData, gridData);
        //Debug.Log(dataToLoad);
        return (true);
    }

    public void Delete() { Delete(gridEditorUi.fileToDelete); }
    public bool Delete(string nameFile)
    {
        Debug.Log("delete" + nameFile);
        if (string.IsNullOrEmpty(nameFile))
            return (false);
        //ici delete le fichier nameFile si il existe
        //ici;
        var pathSavedMaps = $"{editorPath}/{pathMaps}/{nameFile}.{extentionFile}";
        if (!File.Exists(pathSavedMaps))
            return (false);

        ExtFile.DeleteFromEditor(pathSavedMaps);
        //File.Delete(pathSavedMaps);

        savedFiles.Remove(nameFile);


        gridEditorUi.InitSavedMapDropDown(savedFiles);
        return (true);
    }

}
