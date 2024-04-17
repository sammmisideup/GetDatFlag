using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class EggBombGun : NetworkBehaviour
{
    public NetworkVariable<int> ammo = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone);

    [SerializeField] 
    private GameObject bullet;

    [SerializeField] 
    private Transform initialTransform;
    
    [SerializeField] 
    private NetworkVariable<float> fireRate = new NetworkVariable<float>(0.4f, NetworkVariableReadPermission.Everyone);
    
    [SerializeField] private NetworkVariable<float> timeBetweenShots = new NetworkVariable<float>(0.0f, NetworkVariableReadPermission.Everyone);

    private bool canGainAmmo = true;

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && IsOwner && ammo.Value > 0) // && Time.time > timeBetweenShots.Value - add if you want fire rate
        {
            SpawnBulletServerRpc(initialTransform.position, initialTransform.rotation);
            ammo.Value--;
        }
    }

    [ServerRpc]
    private void SpawnBulletServerRpc(Vector3 position, Quaternion rotation)
    {
        GameObject bulletClone = Instantiate(bullet, position, rotation);
        bulletClone.GetComponent<NetworkObject>().Spawn();
        Destroy(bulletClone, 3f);
    }

    private IEnumerator DelayedAmmoGain()
    {
        canGainAmmo = false;
        yield return new WaitForSeconds(0.5f); // 0.5 seconds delay para di sumobra yung +5 ammo
        ammo.Value += 5; // add ammo
        Debug.Log("Current ammo: " + ammo.Value);
        canGainAmmo = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canGainAmmo && other.CompareTag("Egg"))
        {
            StartCoroutine(DelayedAmmoGain());
        }
    }
}
