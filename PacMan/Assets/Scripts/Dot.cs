using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour
{
    [SerializeField]
    protected int scoreValue = 1;

    public int getScoreValue()
    {
        return scoreValue;
    }

    /// <summary>
    /// If player collide with dot, destroys it
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

}
