using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Utilities;

public class Spells : SerializedMonoBehaviour
{
    
    public SpellType spellType = SpellType.Cross;
    public int levelSpell = 2;

    [SerializeField]
    private List<bool[,]> evolution = new List<bool[,]>();
    [SerializeField]
    private List<Sprite> uiImageSpell;

    public bool[,] GetLevelForArray(int level)
    {
        return (evolution[level]);
    }

    /// <summary>
    /// called when we want the image of the spell
    /// </summary>
    /// <param name="indexLevel"></param>
    /// <returns></returns>
    public Sprite GetImageSpellUI(int index)
    {
        return (uiImageSpell[index]);
    }
}
