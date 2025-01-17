using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class officeTrigger : MonoBehaviour
{
    public GameObject windowCloseTrigger;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //office events: play conversation event in people game obj, play door open sound, move portal obstruction, (move people inside? + close door?)

            windowCloseTrigger.SetActive(true);
        }
    }
}
