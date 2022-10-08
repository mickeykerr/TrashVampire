using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private CheckpointSys cs;
    private bool alreadyCollided;

    public Transform checkpointLocation;
    public Animator animator;
    public AudioSource playCheck;
    // Start is called before the first frame update
    private void Start()
    {
        cs = GameObject.FindGameObjectWithTag("CheckSys").GetComponent<CheckpointSys>();
        alreadyCollided = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            if (cs.lastCheckpoint != new Vector2(checkpointLocation.position.x, checkpointLocation.position.y))
            {
                playCheck.Play();
            }
            cs.lastCheckpoint = checkpointLocation.position;
            animator.SetBool("checkpointOn?", true);

        }
    }
}
