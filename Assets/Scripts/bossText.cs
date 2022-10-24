using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bossText : MonoBehaviour
{
    public GameObject text;
    

    // Start is called before the first frame update
    void Start()
    {
        text.SetActive(false);
        
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            text.SetActive(true);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
