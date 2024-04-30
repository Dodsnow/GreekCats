using CharactersParams;
using GameUI;

public interface ISkill
{
     int manaCost { get; set; }
     string name { get; set; }
     int level { get; set; }
     // ConditionList conditionList { get; set; }

     bool CanCast(PlayableEntity caster)
     {
         return caster.CharacterParameters.CharacterStats[CharactersStats.CurrentMana].CurrentValue >= manaCost;
     }
     
     void ApplySkill() {
         
     }
}