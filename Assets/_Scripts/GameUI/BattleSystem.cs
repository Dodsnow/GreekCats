using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CharactersParams;
using GameUI;
using TMPro;
using UnityEngine;


public class BattleSystem : MonoBehaviour
{
    public BattleState state;
    private PlayableEntity _playerCharacter;
    private PlayableEntity _enemyCharacter;
    private int _currentCharacter = 0;
    public BattleHUD[] playerHUD;
    public BattleHUD[] enemyHUD;
    private List<PlayableEntity> _heroesList;
    private List<PlayableEntity> _enemiesList;
    private List<PlayableEntity> _totalCharacterList;
    public int currentRound = 1;

    [SerializeField] private SpawnSystem _spawnSystem;
    [SerializeField] private TextMeshProUGUI _roundNumber;
    [SerializeField] private HealingManager _healingManager;
    
    public float combatSpeedModificator = 1f;


    public void Awake()
    {
        _roundNumber.text = currentRound.ToString();
    }

    private void Start()
    {
        _totalCharacterList = new List<PlayableEntity>();
        state = BattleState.BattleMapSetup;
        BattleMapSetup();
        BattleRoundInitiave();
        state = BattleState.RoundStart;
    }

    private void FixedUpdate()
    {
        if (state == BattleState.RoundStart)
        {
            StartCoroutine(BattleRoundStart());
        }

        if (state == BattleState.RoundEnd)
        {
            StartCoroutine(BattleRoundEnd());
        }
    }

    void BattleMapSetup()
    {
        _spawnSystem.SpawnEnemies();
        _spawnSystem.SpawnHeroes();
    }

    void BattleRoundInitiave()
    {
        _totalCharacterList.AddRange(_spawnSystem.heroesList);
        _totalCharacterList.AddRange(_spawnSystem.enemiesList);
        _totalCharacterList = _totalCharacterList
            .OrderBy(x => x.CharacterParameters.CharacterStats[CharactersStats.BattleInitiative].CurrentValue).ToList();
    }


    void CharacterTurn(int currentCharacterIndex)
    {
        PlayableEntity currentCharacter = _totalCharacterList[currentCharacterIndex];

        // _buffManager.ApplyCondition(currentCharacter, currentCharacter.condition);
        if (currentCharacter.characterClass == CharacterClass.Healer)
        {
            List<PlayableEntity> orderedHeroList = new List<PlayableEntity>();
            orderedHeroList.AddRange(_spawnSystem.heroesList);
            orderedHeroList = orderedHeroList
                .OrderBy(x => x.CharacterParameters.CharacterStats[CharactersStats.CurrentHealth].CurrentValue)
                .ToList();

            Debug.Log($"Healable heroes list sorted: {orderedHeroList.Count}");
            for (int i = 0; i < orderedHeroList.Count; i++)
            {
                if (_healingManager.CheckForRemainingHealth(orderedHeroList[i]))
                {
                    int healingValue = _healingManager.Heal(currentCharacter, orderedHeroList[i]);
                    Debug.Log($"I heal you for {healingValue} character {orderedHeroList[i].characterName}");
                    break;
                }
            }
        }
        else if (currentCharacter.characterClass != CharacterClass.Healer)
        {
            Debug.Log($"Character {currentCharacter.characterName} is not a healer");
            if (currentCharacter.skill != null && currentCharacter.skill.CanCast(currentCharacter))
            {
                Debug.Log("Skill is castable");
                if (currentCharacter.skill.TargetType == SkillTargetType.SelfTarget)
                {
                    currentCharacter.skill.ApplySkill(currentCharacter);
                }
                else if (currentCharacter.skill.TargetType == SkillTargetType.SingleTarget)
                {
                    currentCharacter.skill.ApplySkill(currentCharacter, SpawnPoint.FindTarget(currentCharacter));
                    Debug.Log("Single target skill applied");
                }
                else if (currentCharacter.skill.TargetType == SkillTargetType.MultipleTarget)
                {
                    List<PlayableEntity> targetList = new List<PlayableEntity>();
                    targetList.AddRange(_spawnSystem.heroesList);
                    targetList.AddRange(_spawnSystem.enemiesList);
                    currentCharacter.skill.ApplySkill(currentCharacter, targetList);
                }
                else
                {
                    Debug.Log("No target found for skill");
                    DamageManager.DealDamage(currentCharacter, SpawnPoint.FindTarget(currentCharacter));
                }
            }
            else
            {
                Debug.Log("Just Damage");
                DamageManager.DealDamage(currentCharacter, SpawnPoint.FindTarget(currentCharacter));
            }
        }
    }

    IEnumerator BattleRoundStart()
    {
        Debug.Log($"Heroes total:{_spawnSystem.heroesList.Count} Monsters total:{_spawnSystem.enemiesList.Count}");
        state = BattleState.RoundOngoing;
        for (int i = 0; i < _totalCharacterList.Count; i++)
        {
            CharacterTurn(i);
            yield return new WaitForSecondsRealtime(1 * combatSpeedModificator);
        }

        currentRound++;
        _roundNumber.text = currentRound.ToString();
        yield return new WaitForSecondsRealtime(3 * combatSpeedModificator);
        state = BattleState.RoundEnd;
        yield return state;
    }

    IEnumerator BattleRoundEnd()
    {
        BuffManager.DurationUpdate();
        foreach (PlayableEntity heroEntity in _spawnSystem.heroesList)
        {
            if (heroEntity == null || !heroEntity.IsAlive())
            {
                _spawnSystem.heroesList.Remove(heroEntity);
            }
        }

        foreach (PlayableEntity enemyEntity in _spawnSystem.enemiesList)
        {
            if (enemyEntity == null || !enemyEntity.IsAlive())
            {
                _spawnSystem.enemiesList.Remove(enemyEntity);
            }
        }

        if (_spawnSystem.heroesList.Count == 0)
        {
            state = BattleState.Lost;
            EndBattle();
        }
        else if (_spawnSystem.enemiesList.Count == 0)
        {
            state = BattleState.Won;
            EndBattle();
        }
        else
        {
            state = BattleState.RoundStart;
        }

        yield return state;
    }


    void EndBattle()
    {
        if (state == BattleState.Won)
        {
            Debug.Log("You won the battle");
        }
        else if (state == BattleState.Lost)
        {
            Debug.Log("You lost the battle");
        }
    }
}