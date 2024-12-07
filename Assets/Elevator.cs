using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public GameStateManager gameStateManager;

    public GameObject doorSoundSourceLift;
    public GameObject doorSoundSourceLevel;
    public GameObject bellSoundSource;
    public GameObject rideSoundSource;

    public doorAnim doorAnim;

    public bool doorClosed;
    public float doorCloseDelay = 2f;

    public float dingDelay = 3f;

    public float rideDuration;

    private bool playerAlreadyChecked;

    private void Start()
    {
        doorClosed = false;
    }

    private void Close()
    {
        Debug.Log("Door Closing");

        doorAnim.DoorClose();
        AkSoundEngine.PostEvent("lift_close", doorSoundSourceLift);
        //AkSoundEngine.PostEvent("lift_close", doorSoundSourceLevel);

        StartCoroutine(WaitForDoorClose());

    }

    private IEnumerator WaitForDoorClose()
    {
        yield return new WaitForSeconds(doorCloseDelay);
        doorClosed = true;

    }


    public void Ride()
    {
        Debug.Log("Start Lift Ride");

        AkSoundEngine.PostEvent("lift_ride", rideSoundSource);
    }

    public void Open()
    {

        Debug.Log("Door Opening");
        AkSoundEngine.PostEvent("lift_ding", bellSoundSource);
        StartCoroutine(DelayAfterDing());

    }

    private IEnumerator DelayAfterDing()
    {
        yield return new WaitForSeconds(dingDelay);

        doorAnim.DoorOpen();
        AkSoundEngine.PostEvent("lift_open", doorSoundSourceLift);
        //AkSoundEngine.PostEvent("lift_open", doorSoundSourceLevel);
        doorClosed = false;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !playerAlreadyChecked)
        {
            playerAlreadyChecked = true;

            gameStateManager.insideLift = true;
            Debug.Log("Player is inside Lift.");

            if (gameStateManager.currentLevelComplete)
            {
                Debug.Log("Current Level is Complete");

                StartCoroutine(HandleElevatorSequence());
            }
        }
    }


    private IEnumerator HandleElevatorSequence()
    {
        yield return new WaitForSeconds(doorCloseDelay);

        Debug.Log("Start Elevator Sequence");

        // Close the door
        Debug.Log("Closing the Door");
        Close(); 
        yield return new WaitForSeconds(doorCloseDelay); // Wait for the door-closing duration

        Debug.Log("Door Closed");

        // Start the ride after the door is fully closed
        Debug.Log("Start Ride.");
        Ride(); 



        gameStateManager.TriggerLevelChange();
        Debug.Log("Wait for Ride to End.");
        yield return new WaitForSeconds(rideDuration);

      
        Debug.Log("Open Door.");
        Open();

        playerAlreadyChecked = false; // Reset the flag to allow reuse
    }

}