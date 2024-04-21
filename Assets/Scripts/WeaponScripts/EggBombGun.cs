using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class EggBombGun : NetworkBehaviour
{
    public NetworkVariable<int> ammo = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    [SerializeField] 
    private GameObject bullet;

    [SerializeField] 
    private Transform initialTransform;
    
    [SerializeField] 
    private NetworkVariable<float> fireRate = new NetworkVariable<float>(0.4f, NetworkVariableReadPermission.Everyone);
    
    [SerializeField] private NetworkVariable<float> timeBetweenShots = new NetworkVariable<float>(0.0f, NetworkVariableReadPermission.Everyone);

    // // private GameObject bulletClone;    

    private bool canGainAmmo = true;
    [SerializeField] private List<GameObject> spawnedBullets = new List<GameObject>();

    void Start()
    {
        ammo.Value = 0;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && IsOwner) // && Time.time > timeBetweenShots.Value - add if you want fire rate
        {
            SpawnBulletServerRpc();
            
            if(ammo.Value > 0)
            {
                ammo.Value--; 
            }
        }
    }

    [ServerRpc]
    private void SpawnBulletServerRpc()
    {
        if(ammo.Value > 0)
        {
            GameObject bulletClone = Instantiate(bullet, initialTransform.position, initialTransform.rotation);
            spawnedBullets.Add(bulletClone);
            bulletClone.GetComponent<NewBulletScript>().parent = this;
            bulletClone.GetComponent<NetworkObject>().Spawn();
            Destroy(bulletClone, 3f);
        }

        if(ammo.Value == 0)
        {
            return;
        }
    }



    [ServerRpc(RequireOwnership = false)]
    public void DestroyServerRpc()
    {
        GameObject toDestroy = spawnedBullets[0];
        toDestroy.GetComponent<NetworkObject>().Despawn();
        spawnedBullets.Remove(toDestroy);
    }
    // private IEnumerator DelayedAmmoGain()
    // {
    //     canGainAmmo = false;
    //     yield return new WaitForSeconds(0.5f); // 0.5 seconds delay para di sumobra yung +5 ammo
    //     ammo.Value += 5; // add ammo
    //     Debug.Log("Current ammo: " + ammo.Value);
    //     canGainAmmo = true;
    // }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (canGainAmmo && other.CompareTag("Egg"))
    //     {
    //         StartCoroutine(DelayedAmmoGain());
    //     }
    // }

}
