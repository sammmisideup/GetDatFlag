using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class GameOverScript : NetworkBehaviour
{
    public TextMeshProUGUI WTeam;
    public TextMeshProUGUI WScore;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        WTeam.text = PlayerPrefs.GetString("WinningTeam");
        WScore.text = PlayerPrefs.GetString("WinningScore");
        NetworkManager.Singleton.Shutdown();
        DestroyNetworkManager.instance.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartGame()
    {
        // PlayerPrefs.DeleteKey("WinningScore"); // TO TEST
        // PlayerPrefs.DeleteKey("WinningTeam");
        SceneManager.LoadScene("Lobby");
    }
}
