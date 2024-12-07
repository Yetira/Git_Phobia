using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerLift : MonoBehaviour
{
    public int player;

    public Elevator elevator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == player)
        {
            elevator.Open();
        }
    }
}
