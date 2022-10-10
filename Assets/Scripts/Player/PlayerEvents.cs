using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

//this is a placeholder script, will need to reconcile and make similar to PlayerAttack + EnemyEvents

public class PlayerEvents : MonoBehaviour
{
    //checkpoint

    private CheckpointSys cs;
    //invincibility check
    private float invincibilityTimer;
    public float invincibilityTimerMax = 1;
    public bool invincible;

    //Enemy DMG (placeholder)
    public float EnemDMG = 2.5f;

    public PlayerHealth player;

    public GameObject restartText;
    public GameObject winText;
    public string nextLevel;

    public AudioSource ouchAudio;

    private bool onlyOnce;
    private bool win;
    private float winTime = 5;
    private float counter;

    public UnityEvent onHit;
    public Rigidbody2D playerRigidbody;
    private float playerSpeed;
    public float boost;

    // Start is called before the first frame update
    void Start()
    {
        cs = GameObject.FindGameObjectWithTag("CheckSys").GetComponent<CheckpointSys>();
        playerSpeed = GetComponent<PlayerMovement>().nyoom;

        invincible = false;
        invincibilityTimer = 0;

        restartText.SetActive(false);
        winText.SetActive(false);
        onlyOnce = false;
        win = false;
        counter = 0;

        transform.position = cs.lastCheckpoint;
    }

    // Update is called once per frame
    void Update()
    {
        if (invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;
        }
        if (invincibilityTimer <= 0)
        {
            invincibilityTimer = 0;
            invincible = false;
        }

        if (player.isDead)
        {
            restartText.SetActive(true);
            if (!onlyOnce)
            {
                ouchAudio.Play();
                onlyOnce = true;
            }
        }

        if (win)
        {
            counter += Time.deltaTime;
            if (counter > winTime)
            {
                GameObject musObj = GameObject.FindGameObjectWithTag("music");
                Destroy(musObj);
                GameObject checkObj = GameObject.FindGameObjectWithTag("CheckSys");
                Destroy(checkObj);
                SceneManager.LoadScene(nextLevel);
            }
        }
    }

    //placeholder
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyAttack") && !invincible)
        {
            onHit?.Invoke();
            player.hp -= EnemDMG;
            invincible = true;
            invincibilityTimer = invincibilityTimerMax;
            ouchAudio.Play();
        }
        
        if (collision.gameObject.CompareTag("KillZone"))
        {
            player.hp = 0;
            
            ouchAudio.Play();
        }

        if (collision.gameObject.CompareTag("Win") && !player.isDead)
        {
            winText.SetActive(true);
            win = true;
        }

        if (collision.gameObject.CompareTag("goFast"))
        {
            playerSpeed *= boost * Time.deltaTime;
        }
        
    }
}
