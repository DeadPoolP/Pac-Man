using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public PlayerController _player;
    public UIController ui;
    public int totalDots = 240;
    public Dot exampleDot;
    public BigDot exampleBigDot;
    public int killValue = 10;

    // Start is called before the first frame update
    void Start()
    {
        ui = GetComponent<UIController>();
    }

    // Update is called once per frame
    void Update()
    {
        ui.UpdateLives(_player.health);
        ui.UpdateScore(_player.dotsEaten * exampleDot.getScoreValue() + _player.bigDotsEaten * exampleBigDot.getScoreValue() + _player.killCount * killValue);
        if (_player.dotsEaten == totalDots || _player.health <= 0)
        {
            ui.DisplayGameOverPanel();
        }
    }
}
