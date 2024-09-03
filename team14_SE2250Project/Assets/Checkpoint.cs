using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        if(otherObject.tag == "Player")
        {
            RespawnController.instance.SetSpawn(transform.position);
        }
    }
}
