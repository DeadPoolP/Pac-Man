using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public PlayerController _player;
    private const int totalDots = 312;

 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_player.dotsEaten == totalDots)
        {
            // GG WP
        }
        else
        {
            
        }
    }
}
