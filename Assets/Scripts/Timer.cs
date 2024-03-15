using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Netcode;

public class Timer : NetworkBehaviour
{
    public NetworkVariable<float> timeValue = new NetworkVariable<float>(120, NetworkVariableReadPermission.Everyone);
    private NetworkVariable<int> playerCount = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone);
    public TextMeshProUGUI timerText;

  void Update()
  {
    if(IsServer)
    {
      playerCount.Value = NetworkManager.Singleton.ConnectedClients.Count;
    }


    if(playerCount.Value >= 2)
    {
      UpdateTime();
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

}
