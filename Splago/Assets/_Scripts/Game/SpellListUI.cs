using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellListUI : MonoBehaviour
{
    [SerializeField]
    private GameObject parentListSpells;
    [SerializeField]
    private GameObject parentListBonus;
    [SerializeField]
    private GameObject prefabsSpells;
    [SerializeField]
    private GameObject prefabsBonus;

    private PlayerManager currentPlayer;

    public void Init(PlayerManager _currentPlayer)
    {
        Debug.Log("init");
        currentPlayer = _currentPlayer;
        SetupButtons();
    }

    /// <summary>
    /// at each round, reset button
    /// </summary>
    private void SetupButtons()
    {
        parentListSpells.transform.ClearChild();
        for (int i = 0; i < currentPlayer.spellsPlayer.Count; i++)
        {
            GameObject newSpell = Instantiate(prefabsSpells, parentListSpells.transform);
            ButtonSpellOrBonus buttonScript = newSpell.GetComponent<ButtonSpellOrBonus>();
            buttonScript.Init(this);
            buttonScript.InitSpell(currentPlayer.spellsPlayer[i], currentPlayer);
        }
        parentListBonus.transform.ClearChild();
        //TODO: here set bonus
        /*
        for (int i = 0; i < currentPlayer.spellsPlayer.Count; i++)
        {
            GameObject newSpell = Instantiate(prefabsSpells, parentListSpells.transform);
            ButtonSpellOrBonus buttonScript = newSpell.GetComponent<ButtonSpellOrBonus>();
            buttonScript.Init(this);
            buttonScript.InitBonus();
        }
        */
    }

    /// <summary>
    /// click on a new spell !
    /// </summary>
    public void ClickOnSpell(Spells spell)
    {
        Debug.Log("spell clicked: " + spell.spellType);
        currentPlayer.SetTypeSpell(spell);
    }

    public void ClickOnBonus(int index)
    {
        Debug.Log("bonus clicked: " + index);
    }
}
