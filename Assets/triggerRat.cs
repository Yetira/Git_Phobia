using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerRat : MonoBehaviour
{
    public int playerLayer;

    public Rat rat;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == playerLayer)
        {
            rat.ratRun();
        }
    }
}
