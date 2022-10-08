using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Platformer.Gameplay;
//using static Platformer.Core.Simulation;
//using Platformer.Model;
//using Platformer.Core;

public class Player : MonoBehaviour

{
    //set maxhp, allow hp to be modified by other scripts as necessary
    private float maxhp = 10;
    public float hp;
    public HealthBar healthBar;

    //movement
    Vector2 move;
    // need to stop movement on player death
    public bool movementStatus = true;

    public bool isDead = false;
    //need to have a player death check
    void iAmDead()
    {
        //stop ability to move
        movementStatus = false;
        gameObject.transform.localScale = new Vector3 (1f, 0.5f, 1f);
        if (isDead == false) { gameObject.transform.Translate(0, -.25f, 0); }
        isDead = true;

    }

    // Start is called before the first frame update
    void Start()
    {
        hp = maxhp;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if (movementStatus)
       //{
       //     move.x = Input.GetAxis("Horizontal");
       //}
        //health loss
        if(hp > -1)
        {
            hp -= Time.deltaTime;
        }

        //player death check
        if(hp < 0 && isDead == false)
        {
            iAmDead();
        }

        //set healthbar as appropriate
        healthBar.health = hp;
    }

    //protected override void ComputeVelocity()
    //{
    //}
}
