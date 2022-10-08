using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2DMK controller;
    public PlayerHealth player;
    public Animator animator;
//    public AudioSource moveAudio;
    public AudioSource jumpAudio;

    float horMove = 0f;
    bool jump = false;

    public float nyoom = 40f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.isDead)
        {
            horMove = Input.GetAxisRaw("Horizontal") * nyoom;
//            if(Mathf.Abs(horMove)> 0f && controller.m_Grounded)
//            {
//                moveAudio.Play();
//
//           }
            animator.SetFloat("Speed", Mathf.Abs(horMove));

            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
                animator.SetBool("isJump", true);
                if (controller.m_Grounded) { jumpAudio.Play(); }
            }
        }

    }

    public void OnLand()
    {
        animator.SetBool("isJump", false);
    }

    private void FixedUpdate()
    {
       
        //Les goooooo
        controller.Move(horMove*Time.fixedDeltaTime, false, jump, player.isDead);
        jump = false;
        


    }
}
