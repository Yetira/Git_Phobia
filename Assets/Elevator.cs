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
    public GameObject arrivalSoundSource;

    //public GameObject liftTriggerZone;

    public doorAnim doorAnim;

    public bool doorClosed;
    public float doorCloseDelay = 2f;

    public float dingDelay = 3f;

    public float rideDuration;

    public bool playerAlreadyChecked;

    private void Start()
    {
        doorClosed = false;
    }

    public void Close()
    {
        Debug.Log("Door Closing");

        doorAnim.DoorClose();
        AkSoundEngine.PostEvent("lift_close", doorSoundSourceLift);
        //AkSoundEngine.PostEvent("lift_close", doorSoundSourceLevel);

        StartCoroutine(Wait());

        doorClosed = true;

    }

    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(doorCloseDelay);
        //doorClosed = true;

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

    public void Arrive()
    {
        Debug.Log("Lift arriving.");
        AkSoundEngine.PostEvent("lift_arrive", arrivalSoundSource);

    }
    private IEnumerator DelayAfterDing()
    {
        yield return new WaitForSeconds(dingDelay);

        doorAnim.DoorOpen();
        AkSoundEngine.PostEvent("lift_open", doorSoundSourceLift);
        //AkSoundEngine.PostEvent("lift_open", doorSoundSourceLevel);
        doorClosed = false;

    }


    public  IEnumerator ElevatorSequence()
    {
        yield return new WaitForSeconds(doorCloseDelay);

        Debug.Log("Start Elevator Sequence");

        // Close the door
        Debug.Log("Wait for Door.");
        Close(); 
        yield return new WaitForSeconds(doorCloseDelay); // Wait for the door-closing duration

        Debug.Log("Door Closed.");

        // Start the ride after the door is fully closed
        Debug.Log("Start Ride.");
        Ride();


        if (gameStateManager.currentLevelIndex < gameStateManager.level.Count - 1)
        {
            gameStateManager.TriggerLevelChange();
            
            Debug.Log("Wait for Ride to End.");
            yield return new WaitForSeconds(rideDuration);


            Debug.Log("Open Door.");
            Open();

            playerAlreadyChecked = false; // Reset the flag to allow reuse
        }
        else
        {
            Debug.Log("Now plays the Elevator Ending Sequence!");
            //add music, thank for playing, fade out
        }
    }

}