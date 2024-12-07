using System.Collections;
using System.Collections.Generic;
using UnityEditor.EventSystems;
using UnityEngine;

public class triggerGirl : MonoBehaviour
{
    public int player;

    public girlBehavior girl;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == player)
        {
            girl.girlRun();
        }
    }
}
