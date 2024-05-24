using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;

public class SelectTeam : NetworkBehaviour
{
    [SerializeField] private GameObject player;
    
    [SerializeField] private Renderer playerCloth;
    [SerializeField] private TextMeshProUGUI teamNumber;
    [SerializeField] private TextMeshProUGUI teamNumberOutside;
    
    
    private void OnTriggerEnter(Collider col)
    {
        // if(!IsOwner) return;

        GameObject whatHit = col.gameObject; 

        if(whatHit.CompareTag("SetTeam1"))
        {
            ChangeTeamServerRpc(0);
        }

        if(whatHit.CompareTag("SetTeam2"))
        {
            ChangeTeamServerRpc(1);
        }        
    }

    [ServerRpc(RequireOwnership = false)]
    private void ChangeTeamServerRpc(int code)
    {
        ChangeTeamClientRpc(code);
    }

    [ClientRpc]
    private void ChangeTeamClientRpc(int code)
    {
        if(code == 0)
        {
            playerCloth.material.color = new Color32(145, 226, 27, 255);
            ChangeClotheColorClientRpc(0);

            player.gameObject.tag = "Team1";
            Debug.Log("Player set to Team 1");
            teamNumberOutside.text = "Team 1";
            teamNumberOutside.color = new Color32(145, 226, 27, 255);

            if(!IsOwner) return;         
            teamNumber.text = "Team 1";

        }

        if(code == 1)
        {
            playerCloth.material.color = new Color32(226, 158, 27, 255);
            ChangeClotheColorClientRpc(1);

            player.gameObject.tag = "Team2";
            Debug.Log("Player set to Team 2");
            teamNumberOutside.text = "Team 2";
             teamNumberOutside.color = new Color32(226, 158, 27, 255);

            if(!IsOwner) return;
            teamNumber.text = "Team 2";
            
        }
    }    

    [ClientRpc]
    private void ChangeClotheColorClientRpc(int value)
    {
        if(value == 0)
        {
            Debug.Log("Team 1 Player#" + OwnerClientId + " cloth color change 0");    
        }

        if(value == 1)
        {
            Debug.Log("Team 1 Player#" + OwnerClientId + " cloth color change 1");
        }

    }


}
