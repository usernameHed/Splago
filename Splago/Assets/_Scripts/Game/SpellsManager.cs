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

    /// <summary>
    /// get a random list of spells
    /// </summary>
    /// <returns></returns>
    public List<Spells> GetRandomSpells(GameObject player)
    {
        List<Spells> newSpellList = new List<Spells>();
        for (int i = 0; i < allSpells.Count; i++)
        {
            Debug.Log("try to create " + i + " spell"   );
            if (UnityEngine.Random.Range(0, 2) == 1)
            {
                Spells spellsPlayer = Instantiate(allSpells[i], player.transform) as Spells;
                //Spells spellsPlayer = ExtComponent.CopyComponent(allSpells[i], player);
                //spellsPlayer = gameObject.AddComponent<Spells>(allSpells[i]);
                newSpellList.Add(spellsPlayer);
            }
                
        }
        //if list is empty, give him the first power !
        if (newSpellList.Count == 0)
        {
            Spells spellsPlayer = Instantiate(allSpells[0], player.transform) as Spells;
            //spellsPlayer = gameObject.AddComponent<Spells>(allSpells[i]);
            newSpellList.Add(spellsPlayer);
        }


        return (newSpellList);
    }
}
