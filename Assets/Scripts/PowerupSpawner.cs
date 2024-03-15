using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PowerupSpawner : NetworkBehaviour
{
    [SerializeField]
    private Transform[] spawnPoints;
    [SerializeField]
    private GameObject[] pickups;

    private GameObject pickupClone;

    public float timeToSpawn;
    private float currentTimeToSpawn;


    // Start is called before the first frame update
    void Start()
    {
        currentTimeToSpawn = timeToSpawn;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
    }

    private void UpdateTimer()
    {
        if(currentTimeToSpawn > 0)
        {
            currentTimeToSpawn -= Time.deltaTime;

        }
        else{
            SpawnObjectServerRpc();
            currentTimeToSpawn = timeToSpawn;
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void SpawnObjectServerRpc()
    {   
        int randPickup = Random.Range(0, pickups.Length);
        int randSpawnPoint = Random.Range(0, spawnPoints.Length);

        pickupClone = Instantiate(pickups[randPickup], spawnPoints[randSpawnPoint].position, transform.rotation);
        pickupClone.GetComponent<NetworkObject>().Spawn(true);
        Destroy(pickupClone, 8.75f);
        
    }
}