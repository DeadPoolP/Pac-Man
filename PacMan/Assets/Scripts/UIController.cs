using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    [SerializeField]
    private Text scoreValue;
    private GameController _gameController;

    private void Awake()
    {
        _gameController = GetComponent<GameController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreValue.text = _gameController._player.dotsEaten.ToString();
    }
}
