using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class girlBehavior : MonoBehaviour
{

    uint lullabyEventId;
    uint girlRunEventId;

    private Animation animComponent;

    private void Start()
    {
        animComponent = GetComponent<Animation>();

        lullabyEventId = AkSoundEngine.PostEvent("lullaby_event", gameObject);
    }


    public void girlRun()
    {
        AkSoundEngine.StopPlayingID(lullabyEventId);

        //start girlRun audio
        girlRunEventId = AkSoundEngine.PostEvent("girl_run", gameObject);
        Debug.Log("girl starts running.");
        
        //enable animation component and start animation
        //animComponent.enabled = true;

        animComponent.clip = animComponent.GetClip("girl_runAround");
        animComponent["girl_runAround"].time = 0f;
        
        animComponent.Play("girl_runAround");

        StartCoroutine(DisableAnimationAfterPlay());
    }

    private IEnumerator DisableAnimationAfterPlay()
    {
        yield return new WaitForSeconds(animComponent["girl_runAround"].length);
        animComponent.enabled = false;

        AkSoundEngine.StopPlayingID(girlRunEventId);
        gameObject.SetActive(false);
    }
}
