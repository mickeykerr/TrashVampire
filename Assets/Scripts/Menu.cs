using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {            
            GameObject musObj = GameObject.FindGameObjectWithTag("music");
            Destroy(musObj);
            GameObject checkObj = GameObject.FindGameObjectWithTag("CheckSys");
            Destroy(checkObj);   
            SceneManager.LoadScene("StartMenu");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        }
    }
}
