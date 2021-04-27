using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.5f;
    [SerializeField]
    private float _speedMultiplier = 2.0f;
    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private GameObject _shieldVisual, _rightEngine, _leftEngine;
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
    [SerializeField]
    private bool speedBoostActive = false;
    [SerializeField]
    private bool shieldActive = false;

    private SpawnManager _spawnManager;
    [SerializeField]
    private int _score = 0;

    [SerializeField]
    private UIManager _uiManager;

    void Start()
    {
        //current position at start.

        transform.position = new Vector3(0,0,0);

        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if(_spawnManager== null){
            Debug.LogError("The spawnmanager is NULL");
        }
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if(_uiManager== null){
            Debug.LogError("The _uiManager is NULL");
        }
        _shieldVisual.SetActive(false);

        
    }

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

        if(shieldActive){
            shieldActive = false;
            _shieldVisual.SetActive(false);
            return;
        }

        _lives -= 1;
        UpdateDamageVisuals(_lives);
        _uiManager.UpdateLives(_lives);
        if(_lives<1){
            //tell spawn manager that we died so stop spawning.
            _spawnManager.OnPlayerDeath();
            Death();
        }
    }

    private void UpdateDamageVisuals(int _damage){
        switch(_damage){
            case 2:
                _rightEngine.SetActive(true);
            break;
            case 1:
                _leftEngine.SetActive(true);
            break;
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

    public void SpeedPowerUpActive(){
        _speed = _speed * _speedMultiplier;
        speedBoostActive = true;
        StartCoroutine(speedPowerDownRoutine());
    }

    IEnumerator speedPowerDownRoutine(){
        yield return new WaitForSeconds(5);
        _speed = _speed / _speedMultiplier;
        speedBoostActive = false;
    }

    public void ShieldPowerUpActive(){
        _shieldVisual.SetActive(true);
        shieldActive = true;
    }

    public void AddToScore(int points){
        _score += 10;
        //tell UIManager
        _uiManager.UpdateScore(_score);
    }

}
