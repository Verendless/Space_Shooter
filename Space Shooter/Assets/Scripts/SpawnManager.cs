using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
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
        yield return new WaitForSeconds(5.0f);
        Debug.Log("Test");
        while(_spawn == true)
        {
            Vector2 powerUpSpawnLocation = new Vector2(Random.Range(-9.4f, 9.4f), 7.9f);
            Instantiate(_tripleShotPrefab, powerUpSpawnLocation, Quaternion.identity);
            yield return new WaitForSeconds(10.0f);

        }
    }

        public void onPlayerDeath()
    {
        _spawn = false;
    }

}
