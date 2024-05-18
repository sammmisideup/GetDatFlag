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
    [SerializeField] private GameObject weaponSpawner1;
    [SerializeField] private GameObject powerupSpawner;
    [SerializeField] private GameObject powerupSpawner1;
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
        StopSpawners();
        StartSelections();
    }

    private void GetWinner()
    {
        if(timer.timeValue.Value == 0 || TeamScore.team1ScoreNew.Value == 5 || TeamScore.team2ScoreNew.Value == 5)
        {
            timer.timeValue.Value = 0f;
            ShowWinCanvasClientRpc();
            // Loader.LoadNetwork(Loader.Scene.GameOver);

            if(TeamScore.team1ScoreNew.Value > TeamScore.team2ScoreNew.Value)
            {
                winnerText.text = "TEAM 1 WINS!";
                finalScoreText.text = TeamScore.team1ScoreNew.Value + " - " + TeamScore.team2ScoreNew.Value;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;     
                // PlayerPrefs.SetString("WinningTeam", winnerText.text);
                // PlayerPrefs.SetString("WinningScore", finalScoreText.text);
                // Invoke("PauseDelay", 4f);     
            }

            if(TeamScore.team1ScoreNew.Value < TeamScore.team2ScoreNew.Value)
            {
                winnerText.text = "TEAM 2 WINS!";
                finalScoreText.text = TeamScore.team1ScoreNew.Value + " - " + TeamScore.team2ScoreNew.Value;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;     
                // PlayerPrefs.SetString("WinningTeam", winnerText.text);
                // PlayerPrefs.SetString("WinningScore", finalScoreText.text);
                // Invoke("PauseDelay", 4f);
            }            

            if(TeamScore.team1ScoreNew.Value == TeamScore.team2ScoreNew.Value)
            {
                winnerText.text = "DRAW!";
                finalScoreText.text = TeamScore.team1ScoreNew.Value + " - " + TeamScore.team2ScoreNew.Value;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;     
                // PlayerPrefs.SetString("WinningTeam", winnerText.text);
                // PlayerPrefs.SetString("WinningScore", finalScoreText.text);
                // Invoke("PauseDelay", 4f);
            } 


        }
    }

    [ClientRpc]
    public void ShowWinCanvasClientRpc()
    {
        winnerCanvas.SetActive(true);        
    }

    public void DisconnectPlayers()
    {
        BGMPlayer play = FindObjectOfType<BGMPlayer>();
        if(play != null)
        {
            Destroy(play.gameObject);
        }

        if(IsServer)
        {
            Time.timeScale = 1;
            DisconnectHost();
        }

        else if(IsClient)
        {
            Time.timeScale = 1;
            DisconnectClient();
        }
    }

    public void DisconnectHost()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SendPlayersToMenuServerRpc();           
        Disconnect();
    }

    public void DisconnectClient()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;         
        Disconnect();        
    }

    [ServerRpc]
    private void SendPlayersToMenuServerRpc()
    {
        SendPlayersToMenuClientRpc();
    }

    [ClientRpc]
    private void SendPlayersToMenuClientRpc()
    {
        Time.timeScale = 1;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;           
        Disconnect();
        Debug.Log("Server/Host disconnected.");
    }    

    public void Disconnect()
    {
        NetworkManager.Singleton.Shutdown();
        // At this point we must use the UnityEngine's SceneManager to switch back to the MainMenu
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    private void BackToMenu()
    {
        // Loader.LoadNetwork(Loader.Scene.Lobby);
        SceneManager.LoadScene("MainMenu");    
    }

    private void PauseDelay()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;        
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
            powerupSpawner1.GetComponent<PowerupSpawner>().enabled = true;
            weaponSpawner1.GetComponent<PowerupSpawner>().enabled = true;
        }
    }

    private void StopSpawners()
    {
        if(timer.timeValue.Value <= 0)
        {
            flagSpawner.GetComponent<FlagSpawner>().enabled = false;                 // turn off the scripts from the spawners and set time to spawn for flag spawner to 3 seconds
            powerupSpawner.GetComponent<PowerupSpawner>().enabled = false;
            weaponSpawner.GetComponent<PowerupSpawner>().enabled = false;
            powerupSpawner1.GetComponent<PowerupSpawner>().enabled = false;
            weaponSpawner1.GetComponent<PowerupSpawner>().enabled = false;
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
