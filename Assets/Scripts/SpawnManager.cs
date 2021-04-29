using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private Enemy _enemyObject;
    [SerializeField]
    private Powerup[] _powerups;
    [SerializeField]
    private GameObject _enemyContainer;
    private bool shouldSpawn = false;

    IEnumerator SpawnEnemyRoutine()
    {
        while(shouldSpawn)
        {
            Enemy enemy = Instantiate(_enemyObject, new Vector3(0, 9, 0), Quaternion.identity);
            enemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5);
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        while(shouldSpawn)
        {
            int random = Random.Range(0, _powerups.Length);
            Powerup powerup = Instantiate(_powerups[random], new Vector3(0, 9, 0), Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 7));
        }
    }

    public void StartSpawning()
    {
        shouldSpawn = true;
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    public void StopSpawning() 
    {
        shouldSpawn = false;
    }
}
