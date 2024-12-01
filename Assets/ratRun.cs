using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ratRun : MonoBehaviour

{
    [SerializeField] private Vector3 endPosition;   // End position of the rat
    [SerializeField] private float speed = 5f;      // Movement speed adjustable in Inspector

    private bool isRunning = false; // To control if the rat should move

    uint ratRunId;

    private void Start()
    {
        // Set the rat's initial position to the start position
       // transform.position = startPosition;
        
        //ratRunId = AkSoundEngine.PostEvent("rat_kickoff", gameObject);
    }

    private void Update()
    {
        // Check if the rat is running and move it
        if (isRunning)
        {
            Run();
        }
    }

    public void Run()
    {
        // Move the rat towards the end position
        transform.position = Vector3.MoveTowards(transform.position, endPosition, speed * Time.deltaTime);

        ratRunId = AkSoundEngine.PostEvent("rat_kickoff", gameObject);

        // Check if the rat has reached the end position
        if (Vector3.Distance(transform.position, endPosition) < 0.01f)
        {
            isRunning = false; // Stop the movement

            AkSoundEngine.StopPlayingID(ratRunId);
        }
    }

    public void StartRunning()
    {
        // Start the movement
        isRunning = true;
    }
}


