using System.Collections.Generic;
using GameUI;
using Unity.Collections;
using UnityEngine;

public static class BuffManager 
{

    public static Dictionary<PlayableEntity, ActiveCondition> conditionDictionary =
        new Dictionary<PlayableEntity, ActiveCondition>();


    public static void ApplyCondition(PlayableEntity entity, Condition condition)
    {
        
        if (entity != null)
        {
            ActiveCondition activeCondition = new ActiveCondition(condition, condition.conditionDuration);
            foreach (KeyValuePair<PlayableEntity, ActiveCondition> pair in conditionDictionary)
            {
                activeCondition = pair.Value;
                if (activeCondition.Condition.conditionLists == condition.conditionLists)
                {
                    activeCondition.Duration = condition.conditionDuration;
                }

                else
                {
                    conditionDictionary.Add(entity, activeCondition);
                    if (condition.operationFlag)
                    {
                        entity.ModifyPlaybleEntityStatsMultiply(entity, condition.statToModify,
                            condition.conditionValue);
                    }
                    else
                    {
                        entity.ModifyPlaybleEntityStatsAdd(entity, condition.statToModify,
                            (int)condition.conditionValue);
                    }
                }
            }


            Debug.Log(condition.conditionName);
        }
    }

    public static void DurationUpdate()
    {
        foreach (KeyValuePair<PlayableEntity, ActiveCondition> pair in conditionDictionary)
        {
            ActiveCondition activeCondition = pair.Value;

            activeCondition.Duration--;
            if (activeCondition.Duration <= 0)
            {
                RemoveCondition(pair.Key, activeCondition);
            }
        }
    }

    public static void RemoveCondition(PlayableEntity entity, ActiveCondition condition)
    {
        Condition tempCondtion = condition.Condition;
        if (entity == null) return;
        if (conditionDictionary.ContainsKey(entity))
        {
            if (tempCondtion.operationFlag)
            {
                entity.ModifyPlaybleEntityStatsMultiply(entity, tempCondtion.statToModify, -tempCondtion.conditionValue);
            }
            else
            {
                entity.ModifyPlaybleEntityStatsAdd(entity, tempCondtion.statToModify, -(int)tempCondtion.conditionValue);
            }
            conditionDictionary.Remove(entity, out condition);
        }
    }
}