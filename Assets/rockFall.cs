using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class rockFall : MonoBehaviour
{

    public int groundLayer;

    public rat_kickoff rat;

    //public KeyCode resetKey = KeyCode.R;
    private PlayerInputActions playerInputActions;

    private Rigidbody rb;
    private Vector3 startPosition;
    
    uint rockFallId;

 
    private void Awake()
    {
        playerInputActions = new PlayerInputActions();

        playerInputActions.Enable();
    }


    void Start()
    {
        rb = GetComponent<Rigidbody>();

        startPosition = transform.position;

        rb.useGravity = false;
        rb.isKinematic = true;

    }

    private void Update()
    {
        if (playerInputActions.Player.ResetCube.triggered)
        {
            ResetCube();
        }
    }

    private void RockFall()
    {
        rb.useGravity = true;
        rb.isKinematic = false;

        rockFallId = AkSoundEngine.PostEvent("rock_fall", gameObject);
        Debug.Log("rock is falling.");
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == groundLayer)
        {
            AkSoundEngine.PostEvent("rock_land", gameObject);
            Debug.Log("rock has landed.");

            AkSoundEngine.StopPlayingID(rockFallId);

            rat.ratRun();
           
        }
    }
    private void ResetCube()
    {
        transform.position = startPosition;

        RockFall();
       
    }

}

