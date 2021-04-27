using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    bool _isGameOver;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void GameOver(){
        _isGameOver = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && _isGameOver){
            SceneManager.LoadScene(1); //our current game scene is 0 in build settings.
        }
    }
}
