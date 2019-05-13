using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public PlayerController _player;
    public UIController ui;
    public int totalDots = 240;
    public Dot exampleDot;
    public BigDot exampleBigDot;
    public int killValue = 10;
    public bool gameover = false;
    // Start is called before the first frame update
    void Start()
    {
        gameover = false;
        ui = GetComponent<UIController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameover)
        {
            ui.UpdateLives(_player.health);
            ui.UpdateScore(_player.dotsEaten * exampleDot.getScoreValue() + _player.bigDotsEaten * exampleBigDot.getScoreValue() + _player.killCount * killValue);
            if (_player.dotsEaten == totalDots || _player.health <= 0)
            {
                EndGame();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {

                StartGame();
            }
        }
        
        
    }

    private void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    private void EndGame()
    {
        if (_player.health <= 0)
            Destroy(_player.gameObject);
        ui.DisplayGameOverPanel();
        Time.timeScale = 0f;
        gameover = true;
    }
}
