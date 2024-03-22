using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerSpawnPosition : NetworkBehaviour
{
    [SerializeField] private float positionRange;

    public override void OnNetworkSpawn()
    {
        SpawnPositionServerRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    private void SpawnPositionServerRpc()
    {
        transform.position = new Vector3(Random.Range(-72f, -90f), Random.Range(13.7f, 13.7f), Random.Range(-6f, -28f));
        transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
    }


}
