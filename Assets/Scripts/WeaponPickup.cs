using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class WeaponPickup : NetworkBehaviour
{
    [SerializeField] private GameObject weaponPunch;
    [SerializeField] private GameObject weaponClub;
    [SerializeField] private GameObject weaponHammer;

    private void OnTriggerEnter(Collider col)
    {
        // if(!IsOwner) return;

        GameObject whatHit = col.gameObject; 

        if(whatHit.CompareTag("WeaponClub"))
        {
            GiveWeaponServerRpc(0);
        }     

        if(whatHit.CompareTag("WeaponHammer"))
        {
            GiveWeaponServerRpc(1);
        }       
    }

    IEnumerator Club()
    {
        weaponPunch.SetActive(false);
        weaponClub.SetActive(true);
        weaponHammer.SetActive(false);
        ChangeHairClientRpc(1);

        yield return new WaitForSeconds(10f);

        weaponPunch.SetActive(true);
        weaponClub.SetActive(false);
        weaponHammer.SetActive(false);
        ChangeHairClientRpc(0);
    }
    
    IEnumerator Hammer()
    {
        weaponPunch.SetActive(false);
        weaponClub.SetActive(false);
        weaponHammer.SetActive(true);
        ChangeHairClientRpc(2);

        yield return new WaitForSeconds(10f);

        weaponPunch.SetActive(true);
        weaponClub.SetActive(false);
        weaponHammer.SetActive(false);
            
        ChangeHairClientRpc(0);
    }

    [ServerRpc(RequireOwnership = false)]
    private void GiveWeaponServerRpc(int code)
    {
        GiveWeaponClientRpc(code);
    }

    [ClientRpc]
    private void GiveWeaponClientRpc(int code)
    {
        if(code == 0)
        {
            StartCoroutine(Club());
        }

        if(code == 1)
        {
            StartCoroutine(Hammer());
        }
    }


    [ClientRpc]
    private void ChangeHairClientRpc(int value)
    {
        if(value == 0)
        {
            Debug.Log("Player#" + OwnerClientId + " acquired Punch!");            
        }

        if(value == 1)
        {
            Debug.Log("Player#" + OwnerClientId + " acquired a Club!");          
        }

        if(value == 2)
        {
            Debug.Log("Player#" + OwnerClientId + " acquired a Hammer!");        
        }

    }
}
