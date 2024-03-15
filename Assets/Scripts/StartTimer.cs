using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class StartTimer : NetworkBehaviour
{
    [SerializeField]
    private GameObject timerText;

    // Update is called once per frame
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
            timerText.GetComponent<Timer>().enabled = true;
        }

        else
        {
            return;
        }        

    }  

}
