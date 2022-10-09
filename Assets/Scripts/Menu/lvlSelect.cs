using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class lvlSelect : MonoBehaviour
{
    // Start is called before the first frame update
    public void selectLevel(string sumLvl)
    {
        SceneManager.LoadScene(sumLvl);
    }
}
