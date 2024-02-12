using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class MoveCam : NetworkBehaviour
{

    public Transform cameraPosition;

    // Update is called once per frame
    void Update()
    {
        if(!IsOwner) return;
        transform.position = cameraPosition.position;
    }
}
