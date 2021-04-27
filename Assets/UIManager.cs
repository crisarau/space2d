using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    // Start is called before the first frame update

    [SerializeField]
    private Sprite[] _liveImages;
    [SerializeField]
    private Image _currentLifeImage;

    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartText;

    private GameManager _gameManager;
    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _gameOverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if(_gameManager == null){
            Debug.LogError("GAMEMANAGER IS NULL");
        }
    }

    private void Update() {
        
        
    }
    public void UpdateScore(int points){
        _scoreText.text = "Score: " + points.ToString();
    }

    public void UpdateLives(int image){
        _currentLifeImage.sprite = _liveImages[image];
        if(image == 0){
            ShowGameOver();
        }
    }

    private void ShowGameOver(){
        _gameManager.GameOver();
        _gameOverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        //while true make the text = nothing and then something...random seconds...
        StartCoroutine(speedPowerDownRoutine());
        
        
    }
    IEnumerator speedPowerDownRoutine(){
        int timesToFlicker = 100;
        int timesFlickered = 0;
        while(timesFlickered < timesToFlicker){
            yield return new WaitForSeconds(Random.Range(0.05f, 0.30f));
            float chance = Random.Range(0f, 1.0f);
            string textToShow = "";
            textToShow = (chance > 0.5f ?  "" : "Game Over");
            _gameOverText.text = textToShow;
        }

        //the way he did it was while(true) and then .text = gameover yield return 0.5f then ="" then yield return 0.5f again...that simple lol
    }

}
