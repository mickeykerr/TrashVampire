using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    //health declarations
    public float maxhp = 10;
    public float hp;
    public HealthBar healthBar;
    private bool inHealthArea;
    //death declations
    public bool movementStatus = true;
    public bool isDead = false;
    // delete later - placeholder code
    public GameObject AttackCollider;
    public Animator animator;

    public UnityEvent onDeath;
    void iAmDed()
    {
        //stop ability to move
        movementStatus = false;

        //oh no, player fell over
        //gameObject.transform.localScale = new Vector3(1f, 0.5f, 1f);
        //gameObject.GetComponent<CapsuleCollider2D>().offset = new Vector2(0, .5f);
        //if (isDead == false) { gameObject.transform.Translate(0, -.25f, 0); }

        //delete this when colliders stop getting used
        Destroy(AttackCollider);
        onDeath?.Invoke();
        //he dead as hell
        isDead = true;
        animator.SetBool("isDead", true);
    }

    public void healTo(float healAmt)
    {
        
        hp = healAmt;
    }

    //may need to be replaced later
    public void ouch(int ouchAmt)
    {
        hp -= ouchAmt;
    }

    // Start is called before the first frame update
    void Start()
    {
        //set hp to max on level start
        hp = maxhp * 0.75f;
    }

    void Update()
    {
        // hp loss / time
        if (hp < 0 && isDead == false)
        {
            iAmDed();

        
            
        }
        if (hp > -1 && !inHealthArea)
        {
            hp -= Time.deltaTime * 2;
        }

        if (hp > -1 && inHealthArea && hp < 10)
        {
            hp += Time.deltaTime * 10;
        }



        //healthbar should reflect HP
        healthBar.health = hp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Heal Area"))
        {
     
            inHealthArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Heal Area"))
        {
            inHealthArea = false; 
        }
    }
}
