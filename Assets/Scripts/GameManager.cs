using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class GameManager : NetworkBehaviour
{
    [SerializeField]
    private Timer timer;

    [SerializeField]
    private GameObject timerNotice;

    
    void Update()
    {
        if(IsHost)
        {
            HostStartTimeServerRpc();
        }

        else
        {
            return;
        }
        
    }

    
    [ServerRpc]
    private void HostStartTimeServerRpc()
    {
        if(IsHost && Input.GetKeyDown(KeyCode.Return))
        {
            timer.GetComponent<Timer>().enabled = true;
            timerNotice.SetActive(false);
            Debug.Log("Timer started.");
        }

        else
        {
            return;
        }        

    } 
}
