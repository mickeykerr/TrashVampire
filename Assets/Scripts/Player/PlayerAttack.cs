using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public PlayerHealth health;

    public AudioSource hitAudio;
    public AudioSource missAudio;

    public Animator attackAnimationHandler;

    private List<EnemyEvents> _hitEnemies = new List<EnemyEvents>();
    private bool _attacking = false;

    // Update is called once per frame
    void Update()
    {
        if (!_attacking && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("Attacking");
            attackAnimationHandler.SetTrigger("Attack");
            StartCoroutine(Attack(attackAnimationHandler));
        }

        _attacking = attackAnimationHandler.GetCurrentAnimatorStateInfo(0).IsName("Attack");
    }

    private IEnumerator Attack(Animator animation)
    {
        if (_hitEnemies.Count > 0) { hitAudio.Play(); }
        if (_hitEnemies.Count == 0) { missAudio.Play(); }

        do
        {
            foreach (EnemyEvents enemy in _hitEnemies)
            {
                // this line is fine, enemy attacks have not been coded yet.
                if (enemy.isDead == false)
                {
                    health.healTo(health.maxhp);

                    if (TryGetComponent(out PlayerEvents eventManager))
                    {
                        eventManager.flashColor(Color.green, 0.25f);
                    }
                    enemy.killMe();
                }
            }

            yield return new WaitForEndOfFrame();

        } while (animation.GetCurrentAnimatorStateInfo(0).IsName("Attack"));
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
