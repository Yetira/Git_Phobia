using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerRock : MonoBehaviour
{
    public rockFall rock;

    public int playerLayer;

    public float rockFallDelay;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == playerLayer)
        {
            StartCoroutine(DelayRock());
        }
    }

    private IEnumerator DelayRock()
    {
        yield return new WaitForSeconds(rockFallDelay);

        rock.RockFall();
    }
}
