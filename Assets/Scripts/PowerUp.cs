using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    [SerializeField]
    private float _speed = 4.5f;

    //ID for powerup, 0 = triple, 1 = speed, 2 = shield
    [SerializeField]
    private int powerupID;

    [SerializeField]
    private AudioClip _clip;
    
    void Update()
    {
        transform.Translate( Vector3.up * -1.0f * _speed * Time.deltaTime);
        
        if(transform.position.y < -4.5f) {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            Player player = other.transform.GetComponent<Player>();

            //this is a static function that allows us to play a sound, and even if this object is destroyed, which happens immediately, this sound will live on in an object and then die when the sound ends.
            AudioSource.PlayClipAtPoint(_clip, transform.position);

            if(player != null){
                switch(powerupID){
                    case 0:
                        player.TrippleShotActive();
                        break;
                    case 1:
                        //Debug.Log("COLLECTE SPEED");
                        player.SpeedPowerUpActive();
                        break;
                    case 2:
                        player.ShieldPowerUpActive();
                        break;
                    default:
                        break;
                }
            }
            Destroy(this.gameObject);
        }    
    }
}
