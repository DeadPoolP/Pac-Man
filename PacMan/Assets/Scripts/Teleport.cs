using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Teleport exit;
    public Vector3 entryDirection;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") /*|| other.gameObject.CompareTag("Ghost")*/)
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            if (player.transform.forward == entryDirection)
                other.gameObject.transform.position = exit.transform.position;
        }
    }

}
