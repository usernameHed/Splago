using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Utilities;

public class Spells : SerializedMonoBehaviour
{
    
    public SpellType spellType = SpellType.Cross;

    [SerializeField]
    private List<bool[,]> evolution = new List<bool[,]>();

    public bool[,] GetLevel(int level)
    {
        return (evolution[level]);
    }
}
