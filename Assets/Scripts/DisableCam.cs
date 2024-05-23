using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class DisableCam : NetworkBehaviour
{
    [SerializeField] private Camera _camera; // This is your camera, assign it in the prefab
 
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        _camera.enabled = false;

        if (!IsOwner) { return; } // ALL players will read this method, only player owner will execute past this line
        _camera.enabled = true;
    }
    
}
