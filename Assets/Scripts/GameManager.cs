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
    [SerializeField] private GameObject flagSpawner;
    [SerializeField] private GameObject weaponSpawner;
    [SerializeField] private GameObject powerupSpawner;
    [SerializeField] private GameObject teamSelection;
    [SerializeField] private GameObject hairSelection;

    public List<GameObject> playerList;


    void Start()
    {
        winnerText.text = "";
        finalScoreText.text = "";
    }
    
    void Update()
    {
        GetWinner();
        StartSpawners();
        StartSelections();
    }

    private void GetWinner()
    {
        if(timer.timeValue.Value == 0 || TeamScore.team1Score.Value == 3 || TeamScore.team2Score.Value == 3)
        {
            //winnerCanvas.SetActive(true);
            Loader.LoadNetwork(Loader.Scene.GameOver);

            if(TeamScore.team1Score.Value > TeamScore.team2Score.Value)
            {
                winnerText.text = "TEAM 1 WINS!";
                finalScoreText.text = TeamScore.team1Score.Value + " - " + TeamScore.team2Score.Value;
                PlayerPrefs.SetString("WinningTeam", winnerText.text);
                PlayerPrefs.SetString("WinningScore", finalScoreText.text);
                Invoke("PauseDelay", 4f);       
            }

            if(TeamScore.team1Score.Value < TeamScore.team2Score.Value)
            {
                winnerText.text = "TEAM 2 WINS!";
                finalScoreText.text = TeamScore.team2Score.Value + " - " + TeamScore.team1Score.Value;
                PlayerPrefs.SetString("WinningTeam", winnerText.text);
                PlayerPrefs.SetString("WinningScore", finalScoreText.text);
                Invoke("PauseDelay", 4f);
            }            

            if(TeamScore.team1Score.Value == TeamScore.team2Score.Value)
            {
                winnerText.text = "DRAW!";
                finalScoreText.text = TeamScore.team1Score.Value + " - " + TeamScore.team2Score.Value;
                PlayerPrefs.SetString("WinningTeam", winnerText.text);
                PlayerPrefs.SetString("WinningScore", finalScoreText.text);
                Invoke("PauseDelay", 4f);
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

    private void StartSpawners()
    {
        if(timer.timeValue.Value < 300 && timer.timeValue.Value > 299.95)
        {
            flagSpawner.GetComponent<FlagSpawner>().enabled = true;                 // turn off the scripts from the spawners and set time to spawn for flag spawner to 3 seconds
            powerupSpawner.GetComponent<PowerupSpawner>().enabled = true;
            weaponSpawner.GetComponent<PowerupSpawner>().enabled = true;
        }
    }

    private void StartSelections()
    {
        if(timer.timeValue.Value < 345 && timer.timeValue.Value > 299.95)
        {
            hairSelection.SetActive(true);
            teamSelection.SetActive(true);
        }
    }

}
