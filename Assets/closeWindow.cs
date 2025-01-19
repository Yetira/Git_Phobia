using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closeWindow : MonoBehaviour

{
    public Elevator elevator;

    public GameObject window;                 
    public Vector3 windowClosedPosition;     
    public float speed = 5f;             
    private bool isClosing = false;
    private bool isProcessing;

    private void Start()
    {
        isProcessing = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isProcessing)
        {
            isClosing = true;  
            isProcessing = true;
            AkSoundEngine.PostEvent("window_close", window);
        }
    }

    private void Update()
    {
        if (isClosing)
        {
            
            window.transform.localPosition = Vector3.MoveTowards(window.transform.localPosition, windowClosedPosition, speed * Time.deltaTime);


            if (window.transform.localPosition == windowClosedPosition)
            {
                isClosing = false;

                elevator.Arrive();
            }
        }
    }
}

