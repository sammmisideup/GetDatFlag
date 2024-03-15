using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class EggBombGun : NetworkBehaviour
{


    [SerializeField] public NetworkVariable<int> ammo = new NetworkVariable<int>(25, NetworkVariableReadPermission.Everyone);

    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private Transform initialTransform;
    [SerializeField]  private NetworkVariable<float> fireRate = new NetworkVariable<float>(0.4f, NetworkVariableReadPermission.Everyone);


    [SerializeField]  private NetworkVariable<float> timeBetweenShots = new NetworkVariable<float>(0.0f, NetworkVariableReadPermission.Everyone);


    void Update()
    {
        if(Input.GetMouseButtonDown(1) && IsOwner)  // && Time.time > timeBetweenShots.Value - add if you want fire rate
        {
            SpawnBulletServerRpc(initialTransform.position, initialTransform.rotation);
        }
    }

    [ServerRpc]
    private void SpawnBulletServerRpc(Vector3 position, Quaternion rotation)
    {
        
        if(ammo.Value > 0)
        {
            // if(!IsClient || !IsHost) return;
            // timeBetweenShots.Value = Time.time + fireRate.Value;  - add if you want fire rate

            GameObject bulletClone = Instantiate(bullet, position, rotation);
            bulletClone.GetComponent<NetworkObject>().Spawn();
            Destroy(bulletClone, 3f);

            ammo.Value --;
        }
 
    }

}
