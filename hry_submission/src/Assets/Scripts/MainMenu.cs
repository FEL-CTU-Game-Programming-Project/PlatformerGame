using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{//script that activates buttons in the main menu
    
    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void PlayLevel1()
    {
        SceneManager.LoadScene(1);
    }

    public void PlayLevel2()
    {
        SceneManager.LoadScene(4);
    }

    public void PlayLevel1M()
    {
        SceneManager.LoadScene(6);
    }
    public void PlayLevel2M()
    {
        SceneManager.LoadScene(9);
    }

    public void PlayBrawl()
    {
        SceneManager.LoadScene("Brawl1");
    }

    public void PlayBrawlM()
    {
        SceneManager.LoadScene("Brawl1M");
    }




    public void QuitGame()
    {
        Debug.Log ("Quit");
        Application.Quit();
    }
}
