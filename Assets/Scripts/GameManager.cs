using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : NetworkBehaviour
{
    [SerializeField] private Timer timer;
    [SerializeField] private TextMeshProUGUI winnerText;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private GameObject winnerCanvas;


    void Start()
    {
        winnerText.text = "";
        finalScoreText.text = "";
        restartButton.SetActive(false);
    }
    
    void Update()
    {
        GetWinner();
    }

    private void GetWinner()
    {
        if(timer.timeValue.Value == 0 || TeamScore.team1Score.Value == 1 || TeamScore.team2Score.Value == 1)
        {
            //winnerCanvas.SetActive(true);
            Loader.LoadNetwork(Loader.Scene.GameOver);

            if(TeamScore.team1Score.Value > TeamScore.team2Score.Value)
            {
                winnerText.text = "TEAM 1 WINS!";
                finalScoreText.text = TeamScore.team1Score.Value + " - " + TeamScore.team2Score.Value;
                PlayerPrefs.SetString("Winningteam", winnerText.text);
                PlayerPrefs.SetString("WinningScore", finalScoreText.text);
                Invoke("PauseDelay", 4f);       

                if(IsHost)
                {
                    restartButton.SetActive(true);
                }

                else
                {
                    return;
                }

            }

            if(TeamScore.team1Score.Value < TeamScore.team2Score.Value || TeamScore.team2Score.Value == 5)
            {
                winnerText.text = "TEAM 2 WINS!";
                finalScoreText.text = TeamScore.team2Score.Value + " - " + TeamScore.team1Score.Value;
                PlayerPrefs.SetString("Winningteam", winnerText.text);
                PlayerPrefs.SetString("WinningScore", finalScoreText.text);
                Invoke("PauseDelay", 4f);

                if(IsHost)
                {
                    restartButton.SetActive(true);
                }

                else
                {
                    return;
                }          

            }            

            if(TeamScore.team1Score.Value == TeamScore.team2Score.Value)
            {
                winnerText.text = "DRAW!";
                finalScoreText.text = TeamScore.team1Score.Value + " - " + TeamScore.team2Score.Value;
                PlayerPrefs.SetString("Winningteam", winnerText.text);
                PlayerPrefs.SetString("WinningScore", finalScoreText.text);
                Invoke("PauseDelay", 4f);

                if(IsHost)
                {
                    restartButton.SetActive(true);
                }

                else
                {
                    return;
                }          

            } 


        }
    }

    private void PauseDelay()
    {
        Time.timeScale = 0;
    }

    [ClientRpc]
    public void RestartGameClientRpc()
    {
        if(IsServer)
        {
            Time.timeScale = 1;
            NetworkManager.SceneManager.LoadScene("Gameplay", LoadSceneMode.Additive);
            Debug.Log("Restart Game");
        }

    }
}
