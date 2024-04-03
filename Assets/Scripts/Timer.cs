using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Netcode;

public class Timer : NetworkBehaviour
{
    public NetworkVariable<float> timeValue = new NetworkVariable<float>(330, NetworkVariableReadPermission.Everyone);
    private NetworkVariable<int> playerCount = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone);
    public static NetworkVariable<float> timeValuePub = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone);
    public NetworkVariable<float> timeValuePubMakeSure = new NetworkVariable<float>(330, NetworkVariableReadPermission.Everyone);

    public TextMeshProUGUI timerText;

    public GameObject[] playerList;

  void Update()
  {
    if(IsServer)
    {
      playerCount.Value = NetworkManager.Singleton.ConnectedClients.Count;
      timeValuePub.Value = timeValue.Value;
      timeValuePubMakeSure.Value = timeValuePub.Value;
    }



    playerList = GameObject.FindGameObjectsWithTag("Team1");
    playerList = GameObject.FindGameObjectsWithTag("Team2");


    if(LobbyManager.playerLimit == 2)
    {
      if(playerCount.Value == 2)
      {
        UpdateTime();
        // StartGameNotifClientRpc(0);
      }
    }

    if(LobbyManager.playerLimit == 4)
    {
      if(playerCount.Value == 4)
      {
        UpdateTime();
        // StartGameNotifClientRpc(1);
      }
    }

    DisplayTime(timeValue.Value);

   }



  private void UpdateTime()
  {
    if(IsServer)
    {
      if (timeValue.Value > 0)
        {
          timeValue.Value -= Time.deltaTime;
        }

        else
        {
          timeValue.Value = 0;
        }
    }

  }

  
  private void DisplayTime(float timeToDisplay)
  {
    if(IsClient)
    {
      if (timeToDisplay <= 0)
      {
        timeToDisplay = 0;
      }

      float minutes = Mathf.FloorToInt(timeToDisplay / 60);
      float seconds = Mathf.FloorToInt(timeToDisplay % 60);

      timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

      if(timeValue.Value <= 6)
      {
        timerText.color = Color.red;
      }      
    }

  }

  // [ClientRpc]
  // private void StartGameNotifClientRpc(int value)
  // {
  //   if(value == 0)
  //   {
  //     Debug.Log("1v1 is Starting!");    
  //   }

  //   if(value == 1)
  //   {
  //     Debug.Log("2v2 is Starting!");
  //   }    
  // }

}
