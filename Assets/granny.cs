using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class granny : MonoBehaviour
{
    public float grannySleepTime;
    private void Start()
    {
        AkSoundEngine.PostEvent("granny_sleep", gameObject);

        StartCoroutine(WaitForGrannyWakeUp());
    }

    private IEnumerator WaitForGrannyWakeUp()
    {
        yield return new WaitForSeconds(grannySleepTime);

        //AkSoundEngine.PostEvent("stop_granny_sleep", gameObject);

        AkSoundEngine.PostEvent("granny_wakeup", gameObject);
    }
}
