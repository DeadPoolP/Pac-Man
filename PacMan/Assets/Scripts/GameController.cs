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
    public int killValue = 200;
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
            ui.UpdateScore(_player.dotsEaten * exampleDot.getScoreValue() + _player.bigDotsEaten * exampleBigDot.getScoreValue() + _player.killCount * killValue); //calcul the score
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

    /// <summary>
    /// Reload the scene
    /// </summary>
    private void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Pauses the game loop and give the option to restart the game on the UI 
    /// </summary>
    private void EndGame()
    {
        if (_player.health <= 0)
            _player.gameObject.SetActive(false);
        ui.DisplayGameOverPanel();
        Time.timeScale = 0f;
        gameover = true;
    }
}
