using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
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

}
