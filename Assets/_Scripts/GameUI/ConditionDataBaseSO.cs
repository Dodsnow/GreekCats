using System;
using System.Collections.Generic;
using CharactersParams;
using GameUI;
using UnityEngine;


[CreateAssetMenu(fileName = "ConditionDataBase", menuName = "ScriptableObjects/ConditionDataBase", order = 2)]
public  class ConditionDataBaseSo : ScriptableObject
{
 public static List<Condition> conditionsList = new List<Condition>();
    
}

[Serializable]
public class Condition  
{
    public int objectID;
    public bool operationFlag = false;
    public string conditionName = "";
    public int conditionDuration;
    [SerializeField] private int manaCost;
    [field: SerializeField] public CharactersStats statToModify;
    [field: SerializeField] public ConditionList conditionLists;
    [field: SerializeField] public float conditionValue;

  
}





// public ConditionList condition;
        // public int conditionStartCounter = 0;
        // public int conditionFinishCounter = -99999;
        // public int conditionDuration = -99999;
        // public int manaCost;

        
        
       //  Condition(PlayableEntity _entity, ConditionList _condition)
       //  {
       //      condition = _condition;
       //      
       //  }
       //  
       //  public override void ApplyCondition(PlayableEntity entity, ConditionList condition)
       //  {
       //      ApplyMultiplier(entity, condition);
       //      ApplyAdd(entity);
       //  }
       // public void ApplyMultiplier(PlayableEntity entity, ConditionList condition)
       //  {
       //      if (condition == ConditionList.Might)
       //      {
       //          entity.ModifyPlaybleEntityStatsMultiply(CharactersStats.PhysicalDamage, 10);
       //          conditionFinishCounter = 2;
       //          conditionDuration = conditionFinishCounter - conditionStartCounter;
       //          manaCost = 50;
       //      }
       //  }
       //  
       //  void ApplyAdd(PlayableEntity entity)
       //  {
       //      if (condition == ConditionList.LightningReflex)
       //      {
       //          entity.ModifyPlaybleEntityStatsAdd(CharactersStats.PhysicalDamage, 10);
       //          conditionFinishCounter = 2;
       //          conditionDuration = conditionFinishCounter - conditionStartCounter; 
       //          manaCost = 50;
       //      }
       //  }
    
