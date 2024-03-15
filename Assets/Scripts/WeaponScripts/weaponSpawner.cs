using System.Collections;
using UnityEngine;
using Unity.Netcode;

public class weaponSpawner : NetworkBehaviour
{
    public GameObject[] weaponPrefabs;
    public Transform[] spawnTransforms;
    public float spawnInterval = 3f;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnWeaponServerRpc), 0f, spawnInterval);
    }

    [ServerRpc]
    private void SpawnWeaponServerRpc()
    {
        if (weaponPrefabs.Length == 0 || spawnTransforms.Length == 0)
        {
            Debug.LogWarning("No weapon prefabs or spawn transforms assigned to the spawner.");
            return;
        }

        GameObject randomWeaponPrefab = weaponPrefabs[Random.Range(0, weaponPrefabs.Length)];
        Transform randomSpawnTransform = spawnTransforms[Random.Range(0, spawnTransforms.Length)];

        GameObject spawnedWeapon = Instantiate(randomWeaponPrefab, randomSpawnTransform.position, randomSpawnTransform.rotation);

        NetworkObject spawnedNetworkObject = spawnedWeapon.GetComponent<NetworkObject>();
        spawnedNetworkObject.Spawn();

        spawnedNetworkObject.transform.SetParent(transform);
    }
}
