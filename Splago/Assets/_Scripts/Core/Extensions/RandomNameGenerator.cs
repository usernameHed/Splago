using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RandomNameGenerator : SingletonMono<RandomNameGenerator>
{
    [SerializeField]
    private List<string> namesRandom = new List<string>();

    private string pathFile = "RandomNames";
    private string nameFile = "names";


    private bool isInit = false;

    private void Init()
    {
        namesRandom.Clear();
        
        var path = $"{pathFile}/{nameFile}";
        string content = "";
        bool openFile = ExtFile.LoadFromResource(path, ref content);

        string[] lines = content.Split('\n');

        for (int i = 0; i < lines.Length; i++)
        {
            namesRandom.Add(lines[i]);
        }
        isInit = true;
    }

    /// <summary>
    /// return an random names
    /// </summary>
    /// <returns></returns>
    public string GetNames()
    {
        if (!isInit)
            Init();

        return (namesRandom[UnityEngine.Random.Range(0, namesRandom.Count)]);
    }
}
