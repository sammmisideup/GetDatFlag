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
    
    [SerializeField] public static NetworkVariable<int> team1Result = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone);
    [SerializeField] public static NetworkVariable<int> team2Result = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone);  

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        NetworkManager.Singleton.Shutdown();
        DestroyNetworkManager.instance.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        GetWinner();
    }

    private void GetWinner()
    {
            if(TeamScore.team1ScoreNew.Value > TeamScore.team2ScoreNew.Value)
            {
                WTeam.text = "TEAM 1 WINS!";
                WScore.text = TeamScore.team1ScoreNew.Value + " - " + TeamScore.team2ScoreNew.Value;
            }

            if(TeamScore.team1ScoreNew.Value < TeamScore.team2ScoreNew.Value)
            {
                WTeam.text = "TEAM 2 WINS!";
                WScore.text = TeamScore.team1ScoreNew.Value + " - " + TeamScore.team2ScoreNew.Value;
            }            

            if(TeamScore.team1ScoreNew.Value == TeamScore.team2ScoreNew.Value)
            {
                WTeam.text = "DRAW!";
                WScore.text = TeamScore.team1ScoreNew.Value + " - " + TeamScore.team2ScoreNew.Value;
            } 
    }    

    public void RestartGame()
    {
        // PlayerPrefs.DeleteKey("WinningScore"); // TO TEST
        // PlayerPrefs.DeleteKey("WinningTeam");
        SceneManager.LoadScene("Lobby");
    }
}
