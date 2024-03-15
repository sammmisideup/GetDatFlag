using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTeam : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private void OnTriggerEnter(Collider col)
    {
        // if(!IsOwner) return;

        GameObject whatHit = col.gameObject; 

        if(whatHit.CompareTag("SetTeam1"))
        {
         
            player.gameObject.tag = "Team1";
            Debug.Log("Player set to Team 1");

        }

        if(whatHit.CompareTag("SetTeam2"))
        {
         
            player.gameObject.tag = "Team2";
            Debug.Log("Player set to Team 2");

        }        



    }


}
