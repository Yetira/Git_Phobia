using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerLift : MonoBehaviour
{
    public int player;

    public float arriveDuration;

    public Elevator elevator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == player)
        {
            elevator.Arrive();

            StartCoroutine(WaitForLiftToArrive());
        }
    }

    private IEnumerator WaitForLiftToArrive()
    {
        yield return new WaitForSeconds(arriveDuration);
        elevator.Open();
    }
}
