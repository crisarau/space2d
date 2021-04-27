using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.5f;
    
    Player _player;
    
    Animator _animation;

    AudioSource _audioSource;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _animation = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate( Vector3.up * -1.0f * _speed * Time.deltaTime);
        
        if(transform.position.y < -4.5f) {
            transform.position = new Vector3(Random.Range(-9.5f, 9.5f),6.5f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            //damage player

            //nullchecking in case there is no component active.
            Player player = other.transform.GetComponent<Player>();
            if(player != null){
                player.Damage();
            }
            _animation.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioSource.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.8f);
            
        }
        if(other.tag == "Laser"){
            Destroy(other.gameObject);
            if(_player != null){
                _player.AddToScore(10);
            }
            _animation.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioSource.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.8f);
            
        }
    }
}
