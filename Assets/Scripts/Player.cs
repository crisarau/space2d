using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _triplePrefab;
    [SerializeField]
    private Transform _shootingPoint;
    [SerializeField]
    private float _fireRate = 0.25f;

    [SerializeField]
    private int _lives = 3;
    private float _nextFire = 0.0f;    
    private float horizontalInput;
    private float verticalInput;
    [SerializeField]
    private bool tripleShotActive = false;

    private SpawnManager _spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        //current position at start.

        transform.position = new Vector3(0,0,0);

        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if(_spawnManager== null){
            Debug.LogError("The spawnmanager is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        if(Input.GetKeyDown(KeyCode.Space) && Time.time > _nextFire){
            ShootLaser();
        }
        
    }

    void CalculateMovement(){
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        
        //per frame
        //transform.Translate(Vector3.right);
        //3 meters per second
        //transform.Translate(Vector3.right * Time.deltaTime * _speed * horizontalInput);
        //transform.Translate(Vector3.up * Time.deltaTime * _speed * verticalInput);

        //normalizsed to solve the diagona speed boost problem
        Vector3 direction = new Vector3(horizontalInput,verticalInput,0).normalized;
        transform.Translate(direction *  _speed * Time.deltaTime);

        //better way to restrict movement in y axis
        float _yClamp = Mathf.Clamp(transform.position.y, -4, 4);
        transform.position = new Vector3(transform.position.x, _yClamp, 0);
        
        //wrapping around
        if(transform.position.x > 9.5f){
            transform.position = new Vector3(-9.5f,transform.position.y, 0);
        }else if(transform.position.x < -9.5f) {
            transform.position = new Vector3(9.5f,transform.position.y, 0);
        }

    }
    void ShootLaser(){
            
        //the next time it will allow to fire in the future.
        _nextFire = Time.time + _fireRate;

        if(tripleShotActive){
            Instantiate(_triplePrefab, transform.position, Quaternion.identity);
        }else{
            Instantiate(_laserPrefab, _shootingPoint.position, Quaternion.identity);
        }
        
    }

    public void Damage(){
        _lives -= 1;
        if(_lives<1){
            //tell spawn manager that we died so stop spawning.
            _spawnManager.OnPlayerDeath();
            Death();
        }
    }
    private void Death(){
        Destroy(this.gameObject);
    }

    public void TrippleShotActive(){
        tripleShotActive = true;
        StartCoroutine(trippleShotPowerDownRoutine());
    }

    IEnumerator trippleShotPowerDownRoutine(){
        yield return new WaitForSeconds(5);
        tripleShotActive = false; 
    }

}
