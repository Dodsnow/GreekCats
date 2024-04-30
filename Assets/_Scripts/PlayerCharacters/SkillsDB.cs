using System;
using System.Collections.Generic;
using CharactersParams;
using GameUI;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillsDB", menuName = "ScriptableObjects/SkillsDB", order = 3)]
public class SkillsDB : ScriptableObject
{
    public List<Skill> skillsList = new List<Skill>();
    int manaCost { get; set; }
    string name { get; set; }
}

[Serializable]
public class Skill : ISkill
{
    [field: SerializeField] public int manaCost { get; set; }
    [field: SerializeField] public string name { get; set; }
    [field: SerializeField] public int level { get; set; }
    [field: SerializeField] public SkillTargetType TargetType { get; set; }
    // [field: SerializeField] public ConditionList conditionList { get; set; }

    public bool CanCast(PlayableEntity caster)
    {
        return caster.CharacterParameters.CharacterStats[CharactersStats.CurrentMana].CurrentValue >= manaCost;
    }

    public void ApplySkill(PlayableEntity currentCharacter)
    {
    }

    public void ApplySkill(PlayableEntity currentCharacter, PlayableEntity targetCharacter)
    {
    }

    public void ApplySkill(PlayableEntity currentCharacter, List<PlayableEntity> targetCharacters)
    {
    }
}