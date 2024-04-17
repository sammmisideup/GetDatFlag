using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class MinimapScript : NetworkBehaviour
{
    [SerializeField] private GameObject minimapCam;
    [SerializeField] private GameObject minimapCanvas;

    public override void OnNetworkSpawn()
    {
        if(!IsOwner) return;
        minimapCam.SetActive(true);
        minimapCanvas.SetActive(true);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
