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
            StartPositionServerRpc();
        }        

        if(IsOwner && Input.GetKeyDown(KeyCode.R))       // MANUAL TELEPORT or UNSTUCK BUTTON
        {
            if(canTP.Value == true)
            {
                // transform.position = new Vector3(-58f, -2.5f, Random.Range(-20f, -30f));
                StartCoroutine(ResetPlayerPosition());
            }

        }


    }

    IEnumerator ResetPlayerPosition()
    {
        transform.position = new Vector3(0f, 0f, 0f);
        transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        canTP.Value = false;

        yield return new WaitForSeconds(7f);

        canTP.Value = true;
    }

    [ServerRpc(RequireOwnership = false)]
    private void StartPositionServerRpc()
    {
        StartPositionClientRpc();
    }

    [ClientRpc]
    private void StartPositionClientRpc()
    {
        if(!IsOwner) return;
        canTP.Value = true;
        // transform.position = new Vector3(-58f, -2.5f, Random.Range(-20f, -30f));
        transform.position = new Vector3(0f, 0f, 0f);
        transform.rotation = new Quaternion(0f, 0f, 0f, 0f);            
        Debug.Log("Player " + OwnerClientId + "is on starting position");
    }

    



}
