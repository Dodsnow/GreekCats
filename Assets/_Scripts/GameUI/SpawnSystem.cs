using System.Collections.Generic;
using UnityEngine;


public class SpawnSystem : MonoBehaviour
{
    public DataBaseSo dataBaseSo;
    public PointManager pointManager;
    [SerializeField] private GameObject characterSelectionPanel;
    public List<PlayableEntity> heroesList;
    public List<PlayableEntity> enemiesList;

    public void SpawnEnemies()
    {
        int[,] spawnID = {{6,6},{6,7},{8,8},{7,9},{7,11},{7,10}};
        for (int i = 0; i < spawnID.GetLength(0); i++)
        {
            SpawnCharacter(id: spawnID[i, 0], pointManager.characterSpawnPoint[spawnID[i, 1]], PlayerOrEnemy.Enemy);
            
        }
       

    }

    public void SpawnHeroes()
    {
        int[,] spawnID = {{0,0},{1,1},{5,2},{2,3},{3,4},{4,5}};
        for (int i = 0; i < spawnID.GetLength(0); i++)
        {
            SpawnCharacter(id: spawnID[i, 0], pointManager.characterSpawnPoint[spawnID[i, 1]], PlayerOrEnemy.Player);
        }
       
    }


    void SpawnCharacter(int id, GameObject characterSpawnPoint, PlayerOrEnemy playerOrEnemy)
    {
        SpawnPoint spawnPoint = characterSpawnPoint.GetComponent<SpawnPoint>();
        GameObject prefab = Instantiate(dataBaseSo.spawnClassEntities[id].prefab, spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;
        if (prefab != null)
        {
            PlayableEntity entity = prefab.GetComponent<PlayableEntity>();
            entity.CharacterEntityInitiliaze(dataBaseSo.spawnClassEntities[id], playerOrEnemy, spawnPoint, prefab);
            spawnPoint.playableEntity = entity;
            if (playerOrEnemy == PlayerOrEnemy.Player)
            {
                heroesList.Add(entity);
            }
            else
            {
                enemiesList.Add(entity);
            }
        }
    }

    public void ShowCharacterContainer(GameObject currentSelectedObject)
    {
        characterSelectionPanel.transform.position = currentSelectedObject.transform.position;
        characterSelectionPanel.SetActive(true);
    }

    public void HideCharacterContainer()
    {
        characterSelectionPanel.SetActive(false);
    }
}