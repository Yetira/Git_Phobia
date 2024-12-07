using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorTriggerZone : MonoBehaviour
{
    public GameStateManager gameStateManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameStateManager.insideLift = true;
            Debug.Log("Player entered the lift trigger zone.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameStateManager.insideLift = false;
            Debug.Log("Player exited the lift trigger zone.");
        }
    }
}
