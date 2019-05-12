using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    [SerializeField]
    private Text scoreValue;
    [SerializeField]
    private List<GameObject> hps;
    [SerializeField]
    private GameObject gameOverPanel;


    // Start is called before the first frame update
    void Start()
    {
        ResetUI();
    }

    // Update is called once per frame
    void Update()
    {
       

    }

    public void UpdateScore(int score)
    {
        scoreValue.text = score.ToString();

    }

    public void UpdateLives(int health)
    {
        for (int i = 0; i < hps.Count; i++)
        {
            hps[i].SetActive(health > i);
        }
    }

    public void DisplayGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    public void ResetUI()
    {
        foreach (var hp in hps)
        {
            hp.SetActive(true);
        }
        gameOverPanel.SetActive(false);
        scoreValue.text = "00";
    }

}
