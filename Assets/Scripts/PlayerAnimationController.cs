using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator animator;
    public PlayerWeaponManagement PWM;
    public LedgeGrabber ledge;
    public PlayerController PC;

    private void Update()
    {
        if (PC.grounded)
        {
            animator.SetBool("hanging", false);
        }
    }

    public void idle()
    {
        animator.SetBool("run", false);
        animator.SetBool("walk", false);
        animator.SetBool("runWithGun", false);

        if (PWM.hasGun)
        {
            animator.SetBool("idleGun",true);
        }
    }

    public void run()
    {

        if (PWM.hasGun)
        {
            animator.SetBool("run", false);
            animator.SetBool("walk", false);
            animator.SetBool("runWithGun", true);
        }
        else
        {
            animator.SetBool("run", true);
        }
    }

    public void walk()
    {
        if (PWM.hasGun)
        {
            animator.SetBool("run", false);
            animator.SetBool("walk", false);

            animator.SetBool("runWithGun", true);
        }
        else
        {
            animator.SetBool("walk", true);

        }
    }

    public void jump()
    {
        if (PWM.hasGun)
        {
           // animator.Play("Rifle Jump");
        }
        else
        {
            animator.Play("Jump");
           
        }

    }

    public void runAndJump()
    {
        if (PWM.hasGun)
        {
            //animator.Play("Rifle Jump");
        }
        else
        {
            animator.Play("VelJump");

        }
    }

    public void hanging()
    {
        animator.Play("Hanging");
        animator.SetBool("hanging",true);


    }

    public void shoot()
    {
        animator.SetBool("shoot", true);

        animator.Play("GunShoot");

    }


}
