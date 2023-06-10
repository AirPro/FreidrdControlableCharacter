using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerController : MonoBehaviour
{
    // declare a type private animator to refer to at runtime
    private Animator anim;
    // create a variable that enables actions to complete
    // before other actions take place
    private bool accept_input = true;

    public AnimationClip turnLeftAnimClip;

    public AnimationClip turnRightAnimClip;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        if ( turnLeftAnimClip != null || turnRightAnimClip != null) 
        {
            AddInputBlockingAnimEndCallback(turnLeftAnimClip);
            AddInputBlockingAnimEndCallback(turnRightAnimClip);
        }
    }

    private void AddInputBlockingAnimEndCallback(AnimationClip clip)
    {
        AnimationEvent animDoneEvent = new AnimationEvent();
        animDoneEvent.time = clip.length;
        animDoneEvent.functionName = "OnInputBlockingAnimationDone";
        clip.AddEvent(animDoneEvent);
    }

    // Update is called once per frame
    void Update()
    {
        if (accept_input) 
        {
            HandleInput();
        }
    }

    private void OnInputBlockingAnimationDone()
    {
        accept_input = true;
    }

    // Allows the current process to complete before
    // another process can take place
    private void HandleInput()
    {
        anim.SetBool("Crouch", Input.GetKey(KeyCode.C));

        if (Input.GetKey(KeyCode.R)) 
        {
            anim.SetBool("Crouch", false);
            anim.SetTrigger("RightTurn");
            accept_input = false;
        }

        if (Input.GetKey(KeyCode.L)) 
        {
            anim.SetBool("Crouch", false);
            anim.SetTrigger("LeftTurn");
            accept_input = false;
        }
    }
}
