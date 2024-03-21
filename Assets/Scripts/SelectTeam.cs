using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;

public class SelectTeam : NetworkBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private TextMeshProUGUI teamNumber;
    
    private void OnTriggerEnter(Collider col)
    {
        // if(!IsOwner) return;

        GameObject whatHit = col.gameObject; 

        if(whatHit.CompareTag("SetTeam1"))
        {
         
            player.gameObject.tag = "Team1";
            Debug.Log("Player set to Team 1");

            if(!IsOwner) return;         
            teamNumber.text = "Team 1";


        }

        if(whatHit.CompareTag("SetTeam2"))
        {
         
            player.gameObject.tag = "Team2";
            Debug.Log("Player set to Team 2");

            if(!IsOwner) return;
            teamNumber.text = "Team 2";


        }        



    }


}
