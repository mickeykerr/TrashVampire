using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    public PlayerHealth health;

    public AudioSource hitAudio;
    public AudioSource missAudio;

    public Animator attackAnimationHandler;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
//        if (attackAnimationHandler.GetBool("Attack"))
//        {
//            attackAnimationHandler.SetBool("Attack", false);
//        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            attackAnimationHandler.SetTrigger("Attack");
            Attack();
        }
        
    }
    
    void Attack()
    {

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        if(hitEnemies.Length > 0) { hitAudio.Play(); }
        if(hitEnemies.Length == 0) { missAudio.Play(); }
        foreach (Collider2D enemy in hitEnemies)
        {
            // this line is fine, enemy attacks have not been coded yet.
            if(enemy.GetComponent<EnemyEvents>().isDead == false) {health.healTo(health.maxhp);}
            enemy.GetComponent<EnemyEvents>().killMe();
            
        }
    }
    private void OnDrawGizmosSelected()
    {

        if(attackPoint == null) { return; }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
