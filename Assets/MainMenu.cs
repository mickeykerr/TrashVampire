using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    GameObject creditsMenu;
    // Start is called before the first frame update
    
    
    public void playGame()
    {
        SceneManager.LoadScene("Lvl00");
    }

    public void exitGame()
    {
        Application.Quit();
    }

//    public void displayCredits()
//    {
//        this.gameObject.SetActive(false);
//        creditsMenu.SetActive(true);
//    }

}
