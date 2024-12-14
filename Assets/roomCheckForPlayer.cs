using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomCheckForPlayer : MonoBehaviour
{
    public int playerLayer;

    public bool playerInsideRoom;

    private void Start()
    {
        playerInsideRoom = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == playerLayer)
        {
            playerInsideRoom = true;
        }
    }
}
