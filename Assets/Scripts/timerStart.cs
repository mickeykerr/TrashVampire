using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timerStart : MonoBehaviour
{
    Timer timer;
    // Start is called before the first frame update
    private void Awake()
    {
        timer = GameObject.FindGameObjectWithTag("timer").GetComponent<Timer>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("strtTime")){
            timer.startTimer();
        }
    }
}
