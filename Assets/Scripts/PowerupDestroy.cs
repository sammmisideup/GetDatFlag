using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PowerupDestroy : NetworkBehaviour
{

    private GameObject pickupClone;

    // Update is called once per frame
    void Start()
    {
        pickupClone = this.gameObject;
    }


    
    private void OnTriggerEnter(Collider col)
    {
        GameObject whatHit = col.gameObject; 

        if(whatHit.CompareTag("Team1") || whatHit.CompareTag("Team2") || whatHit.CompareTag("Player"))
        {
            DestroyPowerupServerRpc();
        }

    }

    [ServerRpc(RequireOwnership = false)]
    private void DestroyPowerupServerRpc()
    {
        Destroy(pickupClone);
        pickupClone.GetComponent<NetworkObject>().Despawn();
        Debug.Log("Powerup destroyed");        
    }

}
