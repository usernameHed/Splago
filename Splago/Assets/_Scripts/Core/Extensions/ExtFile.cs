using UnityEngine;
using System.IO;
using System;
using System.Text.RegularExpressions;
using System.Text;
using System.Collections.Generic;

/// <summary>
/// Fonctions utile
/// <summary>
public static class ExtFile
{
    #region core script

    /// <summary>
    /// return an nams
    /// </summary>
    /// var path = $"{editorPath}/{pathMaps}/{nameFile}.{extentionFile}";
    public static bool SaveFromEditor(string path, string content)
    {
        if (string.IsNullOrEmpty(path))
            return (false);

        using (StreamWriter writer =
        new StreamWriter(path))
        {
            //writer.WriteLine("first line");
            //writer.WriteLine("second\nAnd third line");
            writer.WriteLine(content);
        }
        return (true);
    }

    /// <summary>
    /// load from resources
    /// </summary>
    public static bool LoadFromResource(string path, ref string content)
    {
        var fileContent = Resources.Load(path) as TextAsset;
        if (fileContent == null)
            return (false);
        content = fileContent.text;
        return (true);
    }

    /// var path = $"{editorPath}/{pathMaps}/{nameFile}.{extentionFile}";
    public static bool LoadFromEditor(string path, ref string content)
    {
        if (!File.Exists(path))
            return (false);

        content = "";
        using (StreamReader sr = File.OpenText(path))
        {
            string s = "";

            while ((s = sr.ReadLine()) != null)
            {
                content += s + "\n";
            }
        }
        return (true);
    }
    /// var path = $"{editorPath}/{pathMaps}/{nameFile}.{extentionFile}";
    public static bool DeleteFromEditor(string path)
    {
        if (!File.Exists(path))
            return (false);
        File.Delete(path);
        return (true);
    }

    #endregion
}
