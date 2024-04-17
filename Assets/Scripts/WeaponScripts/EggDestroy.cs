using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class EggDestroy : NetworkBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (IsServer && (other.CompareTag("Player") || other.CompareTag("Team1") || other.CompareTag("Team2")))
        {
            NetworkObject.Destroy(gameObject);
        }
    }
}
