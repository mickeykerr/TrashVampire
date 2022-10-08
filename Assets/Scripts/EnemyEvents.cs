
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEvents : MonoBehaviour
{
    //death declations
    public bool movementStatus = true;
    public bool isDead = false;
    public float enemDMG;
    // delete later - placeholder code
    public GameObject DamageCollider;

    public CharacterController2DMK controller;
    float horMove;
    public float nyoom = 10f;
    public float moveTime;

    public float curTime;
    bool m_FacingRight = true;
    private Rigidbody2D m_Rigidbody2D;
    private BoxCollider2D m_BoxCollider2D;

    //TRASH CODE CUS SPAGHETTI
    float count = 3;
    // Start is called before the first frame update
    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_BoxCollider2D = GetComponent<BoxCollider2D>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //this is gross
        curTime = Time.realtimeSinceStartup;

        if((Time.realtimeSinceStartup % (moveTime * 2)) < moveTime) { horMove = 1f; }
        if ((Time.realtimeSinceStartup % (moveTime * 2)) > moveTime) { horMove =-1f; }
    }


    private void FixedUpdate()
    {
        if (!isDead)
        {
            Vector3 tVelocity = new Vector2(horMove * nyoom * Time.fixedDeltaTime * 10f, m_Rigidbody2D.velocity.y);
            m_Rigidbody2D.velocity = tVelocity;

            if (horMove > 0 && !m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (horMove < 0 && m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
        }

        else if (isDead)
        {
            
            count -= Time.deltaTime;

            if(count <= 0)
            {
                Destroy(gameObject);
            }

        }
    }
    // kill enemy
    public void killMe()
    {
        //stop ability to move
        movementStatus = false;

        //have enemy pop up, and then fall down for death.
        m_Rigidbody2D.AddForce(new Vector2(0f, 100f));
        //oh no, enemy fell over
        //gameObject.transform.localScale = new Vector3(1f, 0.5f, 1f);
        //gameObject.GetComponent<CapsuleCollider2D>().offset = new Vector2(0, .5f);
        if (isDead == false) { 
            //gameObject.transform.Translate(0, -.25f, 0);

        
        }

        //disable hit
        Destroy(DamageCollider);

        //disable collider
        m_Rigidbody2D.mass = 1;

        m_BoxCollider2D.enabled = false;

        //he dead as hell
        isDead = true;
    }
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
