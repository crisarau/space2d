using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _triplePowerUpPrefab;
    [SerializeField]
    private GameObject _enemyContainer;

    private bool _stopSpawning = false;

    [SerializeField]
    private GameObject[] powerUps;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //spawn a thing every 5 seconds
    //Ienumerator type allows for yield keyword...pauses and returns execution order back to the caller.
    IEnumerator SpawnEnemyRoutine(){
        while(!_stopSpawning){
            GameObject newEnemy = Instantiate(_enemyPrefab, new Vector3(Random.Range(-8f,8f),7.0f), Quaternion.identity);
            //setting it to child of container.
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
    }


    IEnumerator SpawnPowerUpRoutine(){
        while(!_stopSpawning){
            Instantiate(powerUps[Random.Range(0,powerUps.Length)], new Vector3(Random.Range(-8f,8f),7.0f), Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3f,8f)); //3 to 7 seconds
        }
    }
    public void OnPlayerDeath(){
        _stopSpawning = true;
    }

}
