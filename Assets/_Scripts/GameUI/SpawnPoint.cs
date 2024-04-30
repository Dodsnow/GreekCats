using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private Vector2 _spawnPosition;
    [SerializeField] private List<CharacterClass> characterClass;
    public BattleHUD battleHUD;
    public GameObject[] prioribleTargets;
    public GameObject[] healTargets;
    public PlayableEntity playableEntity;


    private void Awake()
    {
        _spawnPosition = new Vector2();
        characterClass = new List<CharacterClass>();
    }

    public static PlayableEntity FindTarget(PlayableEntity currentCharacter)
    {
       
        for (int i = 0; i < 5; i++)
        {
            SpawnPoint damageTargetPoint = currentCharacter.spawnPoint.prioribleTargets[i].GetComponent<SpawnPoint>();
            if (damageTargetPoint.playableEntity)
            {
                return damageTargetPoint.playableEntity; 
            }
        }

        return null;
        
    }
}