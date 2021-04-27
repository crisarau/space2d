using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed = 20.0f;

    [SerializeField]
    private GameObject _explosion;

    private SpawnManager _spawnManager;
    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //rotate object on zed axis
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Laser"){

            Destroy(other.gameObject);
            //damage player
            Instantiate(_explosion,transform);
            _spawnManager.StartSpawning();
            Destroy(this.gameObject, 0.5f);
        }
    }
}
