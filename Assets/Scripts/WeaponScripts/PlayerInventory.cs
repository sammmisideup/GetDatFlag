using System.Collections;
using UnityEngine;
using Unity.Netcode;

public class PlayerInventory : NetworkBehaviour
{
    public Transform handTransform;
    public KeyCode dropKey = KeyCode.F;
    public KeyCode attackKey = KeyCode.G;

    private NetworkObject currentWeapon;

    void Update()
    {
        if (!IsOwner) return;
        {
            if (Input.GetKey(dropKey))
            {
                DropWeaponServerRpc();
            }

            if (Input.GetKey(attackKey))
            {
                AttackWithWeaponServerRpc();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (IsOwner && other.CompareTag("meleeWeapon"))
        {
            NetworkObject networkObject = other.GetComponent<NetworkObject>();
            if (networkObject != null)
            {
                PickUpWeaponServerRpc(networkObject.NetworkObjectId);
            }
        }
    }

    [ServerRpc]
    void PickUpWeaponServerRpc(ulong weaponNetId)
    {
        if (currentWeapon != null)
        {
            DropWeaponServerRpc();
        }

        if (NetworkManager.Singleton.SpawnManager.SpawnedObjects.TryGetValue(weaponNetId, out NetworkObject weaponNetObj))
        {
            weaponNetObj.transform.SetParent(handTransform);
            weaponNetObj.transform.localPosition = Vector3.zero;
            weaponNetObj.transform.localRotation = Quaternion.identity;

            weaponNetObj.GetComponent<Rigidbody>().isKinematic = true;
            weaponNetObj.GetComponent<Collider>().enabled = false;
            weaponNetObj.GetComponent<WeaponScript>().enabled = true;

            currentWeapon = weaponNetObj;
        }
    }

    [ServerRpc]
    void DropWeaponServerRpc()
    {
        if (currentWeapon != null)
        {
            currentWeapon.transform.parent = null;
            currentWeapon.GetComponent<Rigidbody>().isKinematic = false;
            currentWeapon.GetComponent<Collider>().enabled = true;
            currentWeapon.GetComponent<WeaponScript>().enabled = false;

            currentWeapon = null;
        }
    }

    [ServerRpc]
    void AttackWithWeaponServerRpc()
    {
        if (currentWeapon != null)
        {
            Debug.Log("Attacking with the weapon!");
            currentWeapon.GetComponent<WeaponScript>().PerformAttackServerRpc();
        }
    }
}

