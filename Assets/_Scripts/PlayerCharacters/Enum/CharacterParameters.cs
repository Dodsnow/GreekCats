using System;
using System.Collections.Generic;

namespace CharactersParams
{
    public enum CharactersStats
    {
        MaxHealth,
        CurrentHealth,
        MaxMana,
        CurrentMana,
        CharacterLevel,
        PhysicalDamage,
        MagicDamage,
        PhysicalDefense,
        MagicDefense,
        CriticalChance,
        CriticalDamage,
        CriticalDefense,
        BattleInitiative,
    }

    public class CharacterParameters
    {
        public Dictionary<CharactersStats, ParameterGenerator> CharacterStats;
     
        
        public CharacterParameters()
        {
            CharacterStats  = new Dictionary<CharactersStats, ParameterGenerator>();
            foreach (CharactersStats stat in Enum.GetValues(typeof(CharactersStats)))
            {
                CharacterStats.Add(stat, new ParameterGenerator(0));
            }
        }
    }
}