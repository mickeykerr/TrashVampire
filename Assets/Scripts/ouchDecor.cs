using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class ouchDecor : MonoBehaviour
{
    public PlayerHealth player;
    private void Awake()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.hp = 0.01f;
        }
    }
}
