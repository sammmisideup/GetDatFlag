using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class DestroyFlag : NetworkBehaviour
{
    public static DestroyFlag instance;

    private GameObject flagClone;


    void Awake() {
        if (instance == null)
            instance = this;
    }
    
    void Start()
    {
        flagClone = this.gameObject;
        
    }

    private void OnTriggerStay(Collider col)
    {
        GameObject whatHit = col.gameObject; 

        if(whatHit.CompareTag("Team1") || whatHit.CompareTag("Team2"))
        {
            DestroyFlagServerRpc();
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void DestroyFlagServerRpc()
    {
        Destroy(flagClone);  
    }


    

}
