using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject [] _powerUpArray;
    [SerializeField] 
    private GameObject _enemyContainer;
    private bool _spawn = true;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerUpSpawnRoutine());
    }

    IEnumerator EnemySpawnRoutine()
    {
        yield return new WaitForSeconds(2.0f);
        while(_spawn == true)
        {
            Vector2 enemySpawnLocation = new Vector2(Random.Range(-9.4f, 9.4f), 7.9f);
            GameObject newEnemy = Instantiate(_enemyPrefab, enemySpawnLocation, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator PowerUpSpawnRoutine()
    {
        yield return new WaitForSeconds(10.0f);
        while(_spawn == true)
        {
            Vector2 powerUpSpawnLocation = new Vector2(Random.Range(-9.4f, 9.4f), 7.9f);
            Instantiate(_powerUpArray[Random.Range(0,3)], powerUpSpawnLocation, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(5.0f, 10.0f));
        }
    }

        public void onPlayerDeath()
    {
        _spawn = false;
    }

}
