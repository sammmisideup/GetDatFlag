using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class FlagSpawner : NetworkBehaviour
{
    public static FlagSpawner instance;

    public GameObject[] flagPrefabs;
    public Transform[] spawnTransforms;
    public float timeToFlagSpawn;
    private float spawnInterval;

    void Awake() {
        if (instance == null)
            instance = this;
    }

    public void Start()
    {
        spawnInterval = timeToFlagSpawn;
    }

    void Update()
    {
        if(!IsServer) return;
        UpdateFlagTimer();
    }

    private void UpdateFlagTimer()
    {
        if(spawnInterval > 0)
        {
            spawnInterval -= Time.deltaTime;
        }
        if(spawnInterval < 0)
        {
            SpawnFlagServerRpc();
            spawnInterval = 0;
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void SpawnFlagServerRpc()
    {
        if (flagPrefabs.Length == 0 || spawnTransforms.Length == 0)
        {
            Debug.LogWarning("No Flag prefabs or spawn transforms assigned to the spawner.");
            return;
        }

        GameObject randomFlagPrefab = flagPrefabs[Random.Range(0, flagPrefabs.Length)];
        Transform randomSpawnTransform = spawnTransforms[Random.Range(0, spawnTransforms.Length)];

        GameObject spawnedFlag = Instantiate(randomFlagPrefab, randomSpawnTransform.position, randomSpawnTransform.rotation);
        spawnedFlag.GetComponent<NetworkObject>().Spawn(true);
        //spawnedNetworkObject.Spawn();

        //spawnedNetworkObject.transform.SetParent(transform);
    }
}

