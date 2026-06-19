using UnityEngine;

public abstract class SpellSO : CollectibleSO
{
    [Header("General")]
    public float cooldown;

    public override void Collect(Player player) //learn spell when scroll object is collected
    {
        player.magic.LearnSpell(this);
    }
    public abstract void Cast(Player player); //each spell has unique casting effects
}
