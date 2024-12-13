using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Rat : MonoBehaviour
{
    private Animation animComponent;

    void Start()
    {
        animComponent = GetComponent<Animation>();
        animComponent.enabled = false;
    }

    public void ratRun()
    {
        Debug.Log("RAT LOOSE!");

        AkSoundEngine.PostEvent("Play_Rat", gameObject);
        
        animComponent.enabled = true;
        animComponent.clip = animComponent.GetClip("rat_animation");
    
        animComponent.Play("rat_animation");

        StartCoroutine(DisableAnimationAfterPlay());
    }

    private IEnumerator DisableAnimationAfterPlay()
    {
        yield return new WaitForSeconds(animComponent["rat_animation"].length);

        Debug.Log("rat Animation over.");

        AkSoundEngine.PostEvent("Stop_Rat", gameObject);
        
        //gameObject.SetActive(false);
    }
}
