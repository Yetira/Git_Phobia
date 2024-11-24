using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class rat_kickoff : MonoBehaviour
{
    private Animation ratAnim;

    void Start()
    {
        ratAnim = GetComponent<Animation>();
    }

    public void Run()
    {
        Debug.Log("RAT LOOSE!");

        ratAnim.Play("rat_animation");
        //animation.Play();
    }
}
