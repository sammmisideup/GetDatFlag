using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class WeaponCharge : NetworkBehaviour
{
    public NetworkVariable<int> clubCharges = new NetworkVariable<int>(10, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public NetworkVariable<int> stunCharges = new NetworkVariable<int>(3, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    [SerializeField] private GameObject clubWeapon;
    [SerializeField] private GameObject stunWeapon;
    [SerializeField] private GameObject punchWeapon;    

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && IsOwner)
        {
            if(clubWeapon.activeInHierarchy && clubCharges.Value > 0)
            {
                ValueChangeServerRpc(0);
                Debug.Log("Club charge decrease");
            }

            if(stunWeapon.activeInHierarchy && stunCharges.Value > 0)
            {
                ValueChangeServerRpc(1);
                Debug.Log("Stun charge decrease");
            }

            // if(clubWeapon.activeInHierarchy && clubCharges.Value == 0)
            // {
            //     Debug.Log("No charge for Club");
            //     return;
                
            // }

            // if(stunWeapon.activeInHierarchy && stunCharges.Value == 0)
            // {
            //     Debug.Log("No charge for Stun");
            //     return;
                
            // }            
        }

        if(clubCharges.Value == 0 || stunCharges.Value == 0)
        {
            Invoke("ResetDelay", 0.5f);
        }

        if (Input.GetKeyDown(KeyCode.G) && IsOwner) // && Time.time > timeBetweenShots.Value - add if you want fire rate
        {
           DropWeaponServerRpc();
           ValueChangeServerRpc(2);
        }    

    }

    private void ResetDelay()
    {
        ResetWeaponServerRpc();
        ValueChangeServerRpc(2);
    }

    [ServerRpc(RequireOwnership = false)]
    private void ResetWeaponServerRpc()
    {
        ResetWeaponClientRpc();
    }

    [ClientRpc]
    private void ResetWeaponClientRpc()
    {
        clubWeapon.SetActive(false);
        stunWeapon.SetActive(false);
        punchWeapon.SetActive(true);

        Debug.Log("Weapon reset");        
    }

    [ServerRpc(RequireOwnership = false)]
    private void ValueChangeServerRpc(int value)
    {
        if(value == 0)
        {
            clubCharges.Value --;
        }

        if(value == 1)
        {
           stunCharges.Value --; 
        }

        if(value == 2)
        {
            clubCharges.Value = 10;
            stunCharges.Value = 3;
        }        

    }

    [ServerRpc(RequireOwnership = false)]
    private void DropWeaponServerRpc()
    {
        ResetWeaponClientRpc();
    }    

}
