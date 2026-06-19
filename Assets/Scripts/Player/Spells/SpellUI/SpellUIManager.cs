using System.Collections.Generic;
using UnityEngine;

public class SpellUIManager : MonoBehaviour
{
    [SerializeField] private List<SpellSlot> slots = new List<SpellSlot>();

    public void ShowSpells(List<SpellSO> spells) //show all available spells
    {
        for(int i = 0; i < slots.Count; i++)
        {
            if(i < spells.Count)
                slots[i].SetSpell(spells[i]);
            else
                slots[i].SetSpell(null);
        }
    }
    public void HighlightSpell(SpellSO activeSpell) //Higlight spell selected on magic
    {
        foreach(SpellSlot slot in slots)
            slot.SetHighlight(slot.AssignedSpell == activeSpell);
    }

    public void TriggerCooldown(SpellSO spellSO, float cooldownTime) //start individual spellslot cooldown
    {
        foreach(SpellSlot slot in slots)
        {
           if(slot.AssignedSpell == spellSO)
            {
                slot.TriggerCooldown(cooldownTime);
                break;
            }
        }
    }
}
