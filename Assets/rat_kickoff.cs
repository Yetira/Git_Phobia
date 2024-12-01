using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class rat_kickoff : MonoBehaviour
{
    private Animation animComponent;

    uint ratAudioId;

    void Start()
    {
        animComponent = GetComponent<Animation>();
        animComponent.enabled = false;
    }

    public void ratRun()
    {
        Debug.Log("RAT LOOSE!");

        ratAudioId = AkSoundEngine.PostEvent("rat_kickoff", gameObject);
        
        animComponent.enabled = true;
        animComponent.clip = animComponent.GetClip("rat_animation");
    
        animComponent.Play("rat_animation");

        StartCoroutine(DisableAnimationAfterPlay());
    }

    private IEnumerator DisableAnimationAfterPlay()
    {
        yield return new WaitForSeconds(animComponent["rat_animation"].length);
  

        AkSoundEngine.StopPlayingID(ratAudioId);
        
        gameObject.SetActive(false);
    }
}
