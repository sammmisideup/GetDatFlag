using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class DisconnectPlayer : NetworkBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && IsOwner && IsServer) // && Time.time > timeBetweenShots.Value - add if you want fire rate
        {
            DisconnectHost();
        }     

        else if (Input.GetKeyDown(KeyCode.P) && IsOwner) // && Time.time > timeBetweenShots.Value - add if you want fire rate
        {
            DisconnectClient();
        }           
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
            DisconnectHost();
        }

        else if(IsClient)
        {
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

    void DisconnectPlayerAtShutdown(NetworkObject player)
    {   
        NetworkManager.DisconnectClient(player.OwnerClientId);
    }

}
