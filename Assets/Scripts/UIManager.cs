using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    
    public void PlayButton()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void QuitGame()
    {
        Debug.Log("quitters");
        Application.Quit();
    }
    
    public void Settings()
    {
            SceneManager.LoadScene("Settings");
            
    }

    public void Credits()
    {
            SceneManager.LoadScene("Credits");
    }

    public void MainMenu()
    {
            SceneManager.LoadScene("MainMenu");
    }
}
