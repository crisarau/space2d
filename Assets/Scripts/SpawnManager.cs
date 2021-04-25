using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;

    private bool _stopSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SpawnRoutine");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //spawn a thing every 5 seconds
    //Ienumerator type allows for yield keyword...pauses and returns execution order back to the caller.
    IEnumerator SpawnRoutine(){
        while(!_stopSpawning){
            GameObject newEnemy = Instantiate(_enemyPrefab, new Vector3(Random.Range(-8f,8f),7.0f), Quaternion.identity);
            //setting it to child of container.
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
    }
    public void OnPlayerDeath(){
        _stopSpawning = true;
    }

}
