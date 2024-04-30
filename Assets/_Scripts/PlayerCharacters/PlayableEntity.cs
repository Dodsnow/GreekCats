using CharactersParams;
using GameUI;
using UnityEngine;
using Object = UnityEngine.Object;


public class PlayableEntity : MonoBehaviour
{
    public CharacterClass characterClass;
    public string characterName;
    public bool isCreated = false;
    public PlayerOrEnemy playerOrEnemy;
    public SpawnPoint spawnPoint;
    public CharacterParameters CharacterParameters;
    public GameObject prefab;
    public SkillList skillList;
    public Skill skill;


    public void CharacterEntityInitiliaze(SpawnClassEntity dataBaseSo, PlayerOrEnemy playerOrEnemy,
        SpawnPoint spawnPoint, Object prefab)
    {
        skillList = dataBaseSo.skill;
        if (skillList != null)
        {
            switch (skillList)
            {
                case SkillList.PowerStrike:
                    skill = new PowerStrike();
                    break;
                case SkillList.FireBall:
                    // skill = new FireBall();
                    break;
                case SkillList.Heal:
                    // skill = new Heal();
                    break;
                default:
                    skill = null;
                    break;
            }
        }
        else
        {
            skill = null;
        }
       

        characterClass = dataBaseSo.characterClass;
        characterName = dataBaseSo.characterName;
        this.playerOrEnemy = playerOrEnemy;
        this.spawnPoint = spawnPoint;
        CharacterParameters = new CharacterParameters();
        isCreated = true;
        this.prefab = prefab as GameObject;
        foreach (CharactersStats stat in dataBaseSo.characterStats)
        {
            ParameterGenerator parameter;
            if (CharacterParameters.CharacterStats.TryGetValue(stat, out parameter))
            {
                parameter.BaseValue = dataBaseSo.statValue[dataBaseSo.characterStats.IndexOf(stat)];
                parameter.ParameterUpdate();
            }
        }

        CharacterParameters.CharacterStats[CharactersStats.CurrentHealth].CurrentValue =
            CharacterParameters.CharacterStats[CharactersStats.MaxHealth].CurrentValue;
        CharacterParameters.CharacterStats[CharactersStats.CurrentMana].CurrentValue =
            CharacterParameters.CharacterStats[CharactersStats.MaxMana].CurrentValue;
        this.spawnPoint.battleHUD.SetHUD(this);
    }

    public void ModifyPlaybleEntityStatsMultiply(PlayableEntity entity, CharactersStats stat, float multiplier)
    {
        ParameterGenerator generator = entity.CharacterParameters.CharacterStats[stat];
        if (entity.CharacterParameters.CharacterStats.TryGetValue(stat, out generator))
        {
            generator.ModifyValueMultiply(multiplier);
        }
    }


    public void ModifyPlaybleEntityStatsAdd(PlayableEntity entity, CharactersStats stat, int value)
    {
        ParameterGenerator generator = entity.CharacterParameters.CharacterStats[stat];
        generator.ModifyValueAdd(value);
    }


    public bool IsAlive()
    {
        return CharacterParameters.CharacterStats[CharactersStats.CurrentHealth].CurrentValue > 0;
    }

    public void KillEntity()
    {
        Destroy(prefab);
    }
}