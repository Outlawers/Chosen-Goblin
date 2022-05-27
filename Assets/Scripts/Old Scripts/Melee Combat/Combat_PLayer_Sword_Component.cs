using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

/*
 * System responsible for melee combat. 
 */
public class Combat_PLayer_Sword_Component : MonoBehaviour
{

    [SerializeField]
    private Cinemachine.CinemachineVirtualCamera _spellCamera;
    [SerializeField]
    private PlayerInput _inputMap;
    //how often can player attack
    [SerializeField]
    private float cooldownTime = 2f;
    [SerializeField]
    private Canvas _elementalCanvas;

    private InputAction _moveAction;
    private Animator anim;
    //calculate ho many cklicks player input
    public static int noOfClicks = 0;
    //variable to store last input
    float lastClickedTime = 0;



    void Start()
    {
        anim = GetComponent<Animator>();
        _moveAction = _inputMap.actions["Move"];
    }

    public void Attack()
    {
        //if aim camera is turned off
        if(_spellCamera.m_Priority <= 10 && _elementalCanvas.enabled == false)
        {
            //disable movement
            _moveAction.Disable();
            //save the time of the input
            lastClickedTime = Time.time;
            //rise the number of inputs
            noOfClicks++;
            //if there was only one input
            if(noOfClicks == 1)
            {
                //play animation clip
                anim.SetBool("hit1", true);
            }
            noOfClicks = Mathf.Clamp(noOfClicks, 0, 3);

            /*if(noOfClicks >= 2 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit1"))
            {
                anim.SetBool("hit1", false);
                anim.SetBool("hit2", true);
            }
            if (noOfClicks >= 3 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit2"))
            {
                anim.SetBool("hit2", false);
                anim.SetBool("hit3", true);
            }*/
        }
    }

  

    private void DisableAnimation()
    {
        //if animation time is longer than cooldown time and current animation is hit1 or number of cklicks is 0
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > cooldownTime && anim.GetCurrentAnimatorStateInfo(0).IsName("hit1") || noOfClicks == 0)
        {
            //disable hitting animation
            anim.SetBool("hit1", false);
            //Enable movement
            _moveAction.Enable();
        }
        /* if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit2"))
        {
            anim.SetBool("hit2", false);
        }
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit3"))
        {
            anim.SetBool("hit3", false);
            noOfClicks = 0;
        }*/

        //if cooldown time passed 
        if (Time.time - lastClickedTime > cooldownTime)
        {
            //change number of clicks to 0
            noOfClicks = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        DisableAnimation();  
    }
}
