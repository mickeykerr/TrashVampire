using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public PlayerHealth health;

    public AudioSource hitAudio;
    public AudioSource missAudio;
    public float BossDamageAmount = 15;
    public float AttackSpeed = 0.75f;

    public Animator attackAnimationHandler;
    public Collider2D AttackHitbox;

    private List<EnemyEvents> _hitEnemies = new List<EnemyEvents>();
    private List<EnemyEvents> _attackedAlready = new List<EnemyEvents>();

    private bool _attacking = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        _attacking = attackAnimationHandler.GetCurrentAnimatorStateInfo(0).IsName("Attack");
        if (!_attacking && Input.GetKey(KeyCode.Mouse0))
        {
            attackAnimationHandler.SetTrigger("Attack");
            StartCoroutine(Attack(attackAnimationHandler));
            if (_hitEnemies.Count == 0) { missAudio.Play(); }
        }
    }

    private IEnumerator Attack(Animator animation)
    {
        do
        {
            foreach (EnemyEvents enemy in _hitEnemies)
            {
                // this line is fine, enemy attacks have not been coded yet.
                if (enemy.isDead == false && !_attackedAlready.Contains(enemy))
                {
                    _attackedAlready.Add(enemy);
                    health.healTo(health.maxhp);

                    if (TryGetComponent(out PlayerEvents eventManager))
                    {
                        eventManager.flashColor(Color.green, 0.25f);
                    }
                    hitAudio.Play();

                    if (enemy.TryGetComponent(out BossController bossEnemy))
                    {
                        bossEnemy.Damage(BossDamageAmount);
                    }
                    else
                        enemy.killMe();
                }
            }
            yield return new WaitForEndOfFrame();

        } while (animation.GetCurrentAnimatorStateInfo(0).IsName("Attack"));
        _attackedAlready.Clear();
    }

    public void OnBossKill(EnemyEvents controller)
    {
        if (_hitEnemies.Contains(controller)) _hitEnemies.Remove(controller);
        if (_attackedAlready.Contains(controller)) _attackedAlready.Remove(controller);
        StopAllCoroutines();
    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(AttackSpeed);
        _attacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out EnemyEvents enemy))
        {
            if (_attacking) hitAudio.Play();
            _hitEnemies.Add(enemy);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out EnemyEvents enemy))
        {
            _hitEnemies.Remove(enemy);
        }
    }
}
