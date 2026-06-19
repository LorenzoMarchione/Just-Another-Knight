using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class Magic : MonoBehaviour
{
    [Header("References")]
    public Player player;
    public SpellUIManager spellUIManager;

    [Header("Spell Slots")]
    [SerializeField] private List<SpellSO> availableSpells = new List<SpellSO>();
    [SerializeField] private int currentIndex = 0;
    public SpellSO CurrentSpell => availableSpells.Count > 0 ? availableSpells[currentIndex] : null; //if there are available spells select spell in index

    private Dictionary<SpellSO, float> spellCooldowns = new Dictionary<SpellSO, float>(); //spells current cooldowns

    private void Start()
    {
        spellUIManager.ShowSpells(availableSpells);
        HighlightCurrentSpell();
    }
    public void LearnSpell(SpellSO spellSO) //adds spell to list, shows it in ui and selects it if index isn't on other spell
    {
        if (!availableSpells.Contains(spellSO))
            availableSpells.Add(spellSO);
        currentIndex = Mathf.Clamp(currentIndex, 0, availableSpells.Count - 1);

        spellUIManager.ShowSpells(availableSpells);

        if(!spellCooldowns.ContainsKey(spellSO)) //sets current cooldown to 0 when first collected 
            spellCooldowns[spellSO] = 0;

        if (availableSpells.Count > 0) //selects spell in ui if there are no other spells
            HighlightCurrentSpell();
    }
    public void NextSpell() //selects next spell, if index goes beyond spellCount returns to 0
    {
        if (availableSpells.Count == 0)
            return;
        currentIndex = (currentIndex + 1) % availableSpells.Count;
        HighlightCurrentSpell();
    }
    public void PreviousSpell() //selects previous spell, if index goes below spellCount returns to last item
    {
        if (availableSpells.Count == 0)
            return;
        currentIndex = (currentIndex - 1 + availableSpells.Count) % availableSpells.Count;
        HighlightCurrentSpell();
    }
    private void HighlightCurrentSpell() //highligts selected spell in ui
    {
        if(CurrentSpell != null)
            spellUIManager.HighlightSpell(CurrentSpell);
    }
    public void MagicAnimationFinished() //call to player when casting animation is finished and casts spell
    {
        player.AnimationFinished();
        CastSpell();
    }

    public bool CanCast(SpellSO spellSO) //checks for cooldown time
    {
        if(spellSO == null) return false;
        return Time.time >= spellCooldowns[spellSO];
    }
    private void CastSpell() //calls cast of spell and starts cooldown both logic and ui
    {
        if (!CanCast(CurrentSpell) || CurrentSpell == null)
            return;

        CurrentSpell.Cast(player);

        spellCooldowns[CurrentSpell] = Time.time + CurrentSpell.cooldown;
        spellUIManager.TriggerCooldown(CurrentSpell, CurrentSpell.cooldown);
    }
}
