using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerCustomization : NetworkBehaviour
{
    [SerializeField] private GameObject hair1;
    [SerializeField] private GameObject hair2;
    [SerializeField] private GameObject hair3;

    
    private void OnTriggerEnter(Collider col)
    {
        // if(!IsOwner) return;

        GameObject whatHit = col.gameObject; 

        if(whatHit.CompareTag("Hair1"))
        {
            hair1.SetActive(true);
            hair2.SetActive(false);
            hair3.SetActive(false);
            
            ChangeHairClientRpc(0);
        }

        if(whatHit.CompareTag("Hair2"))
        {
            hair1.SetActive(false);
            hair2.SetActive(true);
            hair3.SetActive(false);

            ChangeHairClientRpc(1);
        }     

        if(whatHit.CompareTag("Hair3"))
        {
            hair1.SetActive(false);
            hair2.SetActive(false);
            hair3.SetActive(true);

            ChangeHairClientRpc(2);
        }       
    }


    [ClientRpc]
    private void ChangeHairClientRpc(int value)
    {
        if(value == 0)
        {
            Debug.Log("Player#" + OwnerClientId + " selected Hair #1");            
        }

        if(value == 1)
        {
            Debug.Log("Player#" + OwnerClientId + " selected Hair #2");          
        }

        if(value == 2)
        {
            Debug.Log("Player#" + OwnerClientId + " selected Hair #3");        
        }

    }

}
