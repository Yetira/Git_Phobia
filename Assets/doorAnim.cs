using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorAnim : MonoBehaviour
{
    private Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void DoorOpen()
    {
        animator.SetTrigger("TrOpen");
    }

    public void DoorClose()
    {
        animator.SetTrigger("TrClose");
    }
}
