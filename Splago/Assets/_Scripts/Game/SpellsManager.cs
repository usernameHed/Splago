using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpellType
{
    Cross = 0,
    Square = 1,
    Circle = 2,
    Dotted = 3,
    Spiks = 4
}

public class SpellsManager : SingletonMono<SpellsManager>
{
    [SerializeField]
    private List<Spells> allSpells;

    public Spells GetSpellByType(SpellType type)
    {
        for (int i = 0; i < allSpells.Count; i++)
        {
            if (type == allSpells[i].spellType)
                return (allSpells[i]);
        }
        Debug.LogWarning("no spell ???");
        return (null);
    }
}
