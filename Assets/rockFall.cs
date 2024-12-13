using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class rockFall : MonoBehaviour
{

    public int groundLayer;

    public Rat rat;

    //public KeyCode resetKey = KeyCode.R;
    private PlayerInputActions playerInputActions;

    private Rigidbody rbRock;
    private Vector3 startPosition;

    uint rockFallId;

    private bool rockLanded;


    private void Awake()
    {
        playerInputActions = new PlayerInputActions();

        playerInputActions.Enable();
    }


    void Start()
    {
        rbRock = GetComponent<Rigidbody>();

        startPosition = transform.position;

        rbRock.useGravity = false;
        rbRock.isKinematic = true;

        rockLanded = false;

    }

    private void Update()
    {
        if (playerInputActions.Player.ResetCube.triggered)
        {
            ResetCube();
        }
    }

    public void RockFall()
    {
        if (rockLanded == false)
        {
            rbRock.useGravity = true;
            rbRock.isKinematic = false;

            rockFallId = AkSoundEngine.PostEvent("rock_fall", gameObject);
            Debug.Log("rock is falling.");
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == groundLayer && !rockLanded)
        {
            rockLanded = true;

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

    private IEnumerator WaitToDisableRock()
    {
        yield return new WaitForSeconds(2);

        gameObject.SetActive(false);
    }
    
}

