using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class robotAnimation : MonoBehaviour {

    Animator anim;
    int walkHash = Animator.StringToHash("walk"); //Animation keys for various animations used by the enemy
    int runHash = Animator.StringToHash("running");
    int idleHash = Animator.StringToHash("idle1");
    int punchHash = Animator.StringToHash("punch");
    int deathHash = Animator.StringToHash("death");
    EnemyProperties ep;
    // Use this for initialization
    void Start () {
        ep = GetComponent<EnemyProperties>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if(anim.GetCurrentAnimatorStateInfo(0).fullPathHash == walkHash)
        {
            float move = ep.moveSpeed;
            anim.SetFloat("Speed", move); //Set how fast the animation should play , depending on what type of movement the enemy is using.
        }
        else if(anim.GetCurrentAnimatorStateInfo(0).fullPathHash == runHash)
        {
            float move = ep.moveSpeed;
            anim.SetFloat("Speed", move);
        }

       

       
	}
    public void deathAnimPlay()
    {
        anim.SetBool("death", true); //Start playing the death animation
    }

    public void attackAnimPlay()
    {
        anim.SetBool("isAtTarget", true); //Start playing the attack animation
        anim.SetBool("isWalking", false);
    }

    public void walkAnimPlay()
    {
        anim.SetBool("isAtTarget", false); //Start playing the walk animation
        anim.SetBool("isWalking", true);
    }
}
