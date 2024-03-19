using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class TrapScript : NetworkBehaviour
{
    public float knockbackHeight = 0.5f;
    public float knockbackDistance = 10f;
    public float knockbackForce = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (IsServer)
        {
            if (other.CompareTag("Team1") || other.CompareTag("Team2")|| other.CompareTag("Player"))
            {
                var playerNetworkObject = other.GetComponent<NetworkObject>();
                if (playerNetworkObject != null)
                {
                    ApplyKnockbackServerRpc(playerNetworkObject.NetworkObjectId);
                }
            }
        }
    }

    [ServerRpc]
    void ApplyKnockbackServerRpc(ulong playerNetworkObjectId)
    {
        ApplyKnockbackClientRpc(playerNetworkObjectId);
    }

    [ClientRpc]
    void ApplyKnockbackClientRpc(ulong playerNetworkObjectId)
    {
        var playerNetworkObject = NetworkManager.Singleton.SpawnManager.SpawnedObjects[playerNetworkObjectId];
        if (playerNetworkObject != null)
        {
            var playerGameObject = playerNetworkObject.gameObject;
            Rigidbody playerRigidbody = playerGameObject.GetComponent<Rigidbody>();
            if (playerRigidbody != null)
            {
                Vector3 knockbackDirection = (playerGameObject.transform.position - transform.position).normalized;
                knockbackDirection.y = knockbackHeight;
                playerRigidbody.AddForce(knockbackDirection * knockbackForce * knockbackDistance, ForceMode.Impulse);
            }
        }
    }
}
