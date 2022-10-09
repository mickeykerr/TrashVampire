using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    float curTime;
    public bool start;
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    public void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("timer");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        curTime = 0;
        start = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            curTime += Time.deltaTime;
        }

        double roundedTime = System.Math.Round(curTime, 2);

        text.text = roundedTime.ToString();
    }

    public void startTimer()
    {
        start = true;
    }

    public void stopTimer()
    {
        start = false;
    }
}
