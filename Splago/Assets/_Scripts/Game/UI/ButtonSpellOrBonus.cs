using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSpellOrBonus : MonoBehaviour
{
    [SerializeField]
    private bool isSpell = true;
    [SerializeField]
    private Image imageButton;

    [SerializeField, ReadOnly]
    private SpellListUI refLinkUI;

    [SerializeField, ReadOnly]
    private Spells spellIfSpell;

    public void Init(SpellListUI _refLinkUI)
    {
        refLinkUI = _refLinkUI;
    }

    public void InitSpell(Spells spell, PlayerManager player)
    {
        if (!isSpell)
            return;

        spellIfSpell = spell;

        imageButton.sprite = spellIfSpell.GetImageSpellUI(spellIfSpell.levelSpell);
        imageButton.color = player.GetColorPlayer();
    }
    public void InitBonus()
    {
        if (isSpell)
            return;
    }

    public void CLickOnButton()
    {
        if (isSpell)
            refLinkUI.ClickOnSpell(spellIfSpell);
    }
}
