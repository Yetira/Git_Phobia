using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftTriggerZone : MonoBehaviour
{
    public GameStateManager gameStateManager;
    public Elevator elevator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !elevator.playerAlreadyChecked)
        {
            elevator.playerAlreadyChecked = true;

            gameStateManager.insideLift = true;
            Debug.Log("Player is inside Lift.");

            if (gameStateManager.currentLevelComplete)
            {
                Debug.Log("Current Level is Complete");

               StartCoroutine(elevator.ElevatorSequence());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && gameStateManager.insideLift)
        {
            gameStateManager.insideLift = false;
            StartCoroutine(elevator.Wait());
            elevator.Close();
        }
    }
}
