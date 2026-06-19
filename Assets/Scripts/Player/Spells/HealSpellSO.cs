using UnityEngine;

[CreateAssetMenu(menuName ="Spells/Heal Spell")]
public class HealSpellSO : SpellSO
{
    [Header("Heal Settings")]
    public int healAmount = 10;
    public GameObject healFXPrefab;

    public override void Cast(Player player) //heals player for healAmount
    {
        GameObject newHealFX = Instantiate(healFXPrefab, player.transform.position + Vector3.up * 0.5f, Quaternion.identity);
        Destroy(newHealFX, 2);

        player.health.ChangeHeatlh(healAmount, player.transform.position);
    }
}
