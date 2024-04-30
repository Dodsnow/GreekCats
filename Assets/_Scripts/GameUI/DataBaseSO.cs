using System;
using System.Collections.Generic;
using UnityEngine;
using CharactersParams;
using GameUI;
using Object = UnityEngine.Object;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CharactersDataBase", order = 1)]
public class DataBaseSo : ScriptableObject
{
    public List<SpawnClassEntity> spawnClassEntities;
}

[Serializable]
public class SpawnClassEntity
{
    public int objectID;
    public Object prefab;
    public string characterName = "";
    public CharacterClass characterClass;
    [field: SerializeField]
    public List<CharactersStats> characterStats;
    [field: SerializeField]
    public List<int> statValue;
    [field: SerializeField] public SkillList skill;
}


