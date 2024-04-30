using CharactersParams;
using GameUI;
using UnityEngine;

public class PowerStrike : Skill
{

   int manaCost = 10;
   SkillList skillType = SkillList.PowerStrike;
   SkillTargetType targetType = SkillTargetType.SingleTarget;
    
    public void ApplySkill(PlayableEntity _source, PlayableEntity _target)
    {
        
        if (CanCast(_source))
        {
            _source.CharacterParameters.CharacterStats[CharactersStats.CurrentMana].CurrentValue -= manaCost;
            _source.spawnPoint.battleHUD.SetMp(_source.CharacterParameters.CharacterStats[CharactersStats.CurrentMana].CurrentValue);
            DamageManager.DealDamage(_source, _target);
            BuffManager.ApplyCondition(_target, ConditionDataBaseSo.conditionsList[5]);
            Debug.Log(ConditionDataBaseSo.conditionsList[5].conditionName);


        }
    }
}

