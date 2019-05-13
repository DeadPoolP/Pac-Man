using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Teleport exit;
    public Vector3 entryDirection;

    /// <summary>
    /// Check for player entering portal
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            if (player.transform.forward == entryDirection) // can only enter from one side, preventing infinite teleport
                other.gameObject.transform.position = exit.transform.position;
        }
    }

}
