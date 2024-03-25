using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public TextMeshProUGUI WTeam;
    public TextMeshProUGUI WScore;
    // Start is called before the first frame update
    void Start()
    {
        DestroyNetworkManager.instance.enabled = true;
        WTeam.text = PlayerPrefs.GetString("WinningTeam");
        WScore.text = PlayerPrefs.GetString("WinningScore");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Lobby");
    }
}
