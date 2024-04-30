using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Netcode;

public class TeamScore : NetworkBehaviour
{
    [SerializeField] private TextMeshProUGUI team1Text;
    [SerializeField] private TextMeshProUGUI team2Text;

    [SerializeField] public static NetworkVariable<int> team1Score = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    [SerializeField] public static NetworkVariable<int> team2Score = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    [SerializeField] public static NetworkVariable<int> team1ScoreNew = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone);
    [SerializeField] public static NetworkVariable<int> team2ScoreNew = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone);


    void Update()
    {
        DisplayScore();
    }
    
    private void DisplayScore()
    {
        
        team1Text.text = team1ScoreNew.Value + " :Team 1";
        team2Text.text = "Team 2: " + team2ScoreNew.Value;  
    }



}
