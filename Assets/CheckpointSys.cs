using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSys : MonoBehaviour
{

    public Vector2 lastCheckpoint;

    private void Awake()
    {

        GameObject[] objs = GameObject.FindGameObjectsWithTag("CheckSys");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void CheckpointUpdate()
    {
        
    }
}
