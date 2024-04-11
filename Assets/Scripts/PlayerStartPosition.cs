using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerStartPosition : NetworkBehaviour
{
    public NetworkVariable<bool> canTP = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    void Update()
    {

        if(IsOwner && Timer.timeValuePub.Value < 300 && Timer.timeValuePub.Value > 299.95) // SA WAKAS GUMANA NA
        {
            canTP.Value = true;
            transform.position = new Vector3(-58f, -2.5f, Random.Range(-20f, -30f));
            transform.rotation = new Quaternion(0f, 0f, 0f, 0f);            
            Debug.Log("Starting Position");
        }        

        // if(isReady.Value == true)
        // {
        //     transform.position = new Vector3(-58f, -2.5f, Random.Range(-20f, -30f));
        //     transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        //     Debug.Log("way 1");              
        // }

        // if(isReady.Value == true)
        // {
        //     StartPositionServerRpc();
        //     Debug.Log("way 2");    
        // }

        if(IsOwner && Input.GetKeyDown(KeyCode.R))       // MANUAL TELEPORT or UNSTUCK BUTTON
        {
            if(canTP.Value == true)
            {
                transform.position = new Vector3(-58f, -2.5f, Random.Range(-20f, -30f));
                transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
            }

        }


    }

    [ServerRpc(RequireOwnership = false)]
    private void StartPositionServerRpc()
    {
        transform.position = new Vector3(-58f, -2.5f, Random.Range(-20f, -30f));
        transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
    }

    [ClientRpc]
    private void StartPositionClientRpc()
    {
        transform.position = new Vector3(-58f, -2.5f, Random.Range(-20f, -30f));
        transform.rotation = new Quaternion(0f, 0f, 0f, 0f);   
    }



}
