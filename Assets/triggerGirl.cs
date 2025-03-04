using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class triggerGirl : MonoBehaviour
{
    public int player;

    public girlBehavior girl;

    private bool isProcessing;

    private void Start()
    {
        isProcessing = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == player && !isProcessing)
        {
            isProcessing = true;
            girl.girlRun();
        }
    }
}
