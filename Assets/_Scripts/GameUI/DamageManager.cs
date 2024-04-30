using CharactersParams;
using UnityEngine;
using Random = System.Random;

public static class DamageManager
{
    public static void DealDamage(PlayableEntity source, PlayableEntity target)
    {
        int ongoingDamage = -9999;
        int pureDamage= -9999;
        int defense = -9999;
        int criticalDamage = -9999;
        int criticalDefense = -9999;
        // True for physical, false for magical
        bool damageType = source.CharacterParameters.CharacterStats[CharactersStats.MagicDamage].CurrentValue <
                          source.CharacterParameters.CharacterStats[CharactersStats.PhysicalDamage].CurrentValue;


        if (damageType)
        {
            ongoingDamage = source.CharacterParameters.CharacterStats[CharactersStats.PhysicalDamage].CurrentValue;
            defense = target.CharacterParameters.CharacterStats[CharactersStats.PhysicalDefense].CurrentValue;
        }
        else
        {
            ongoingDamage = source.CharacterParameters.CharacterStats[CharactersStats.MagicDamage].CurrentValue;
            defense = target.CharacterParameters.CharacterStats[CharactersStats.MagicDefense].CurrentValue;
        }

        if (CriticalCheck(source))
        {
            criticalDefense = target.CharacterParameters.CharacterStats[CharactersStats.CriticalDefense].CurrentValue;
            criticalDamage = ongoingDamage * 2 - criticalDefense;
            target.CharacterParameters.CharacterStats[CharactersStats.CurrentHealth].CurrentValue -= criticalDamage;
            target.spawnPoint.battleHUD.SetHp(target.CharacterParameters.CharacterStats[CharactersStats.CurrentHealth]
                .CurrentValue);
            Debug.Log($"Source {source.characterName} Target {target.characterName} Damage {criticalDamage} Critical");
        }
        else
        {
            pureDamage = ongoingDamage - defense;
            if (pureDamage < defense * 0.2)
            {
                pureDamage = (int)(defense * 0.2);
            }
            target.CharacterParameters.CharacterStats[CharactersStats.CurrentHealth].CurrentValue -= pureDamage;
            target.spawnPoint.battleHUD.SetHp(target.CharacterParameters
                .CharacterStats[CharactersStats.CurrentHealth].CurrentValue);
            Debug.Log($"Source: {source.characterName} Target: {target.characterName} Damage: {pureDamage}");
        }

        if (target.CharacterParameters.CharacterStats[CharactersStats.CurrentHealth].CurrentValue <= 0)
        { 
            
            target.KillEntity();
        }
    }

    static bool CriticalCheck(PlayableEntity source)
    {
        Random range = new Random();
        int criticalRangeRoll = range.Next(0, 100);
        return criticalRangeRoll <=
               source.CharacterParameters.CharacterStats[CharactersStats.CriticalChance].CurrentValue;
    }
}