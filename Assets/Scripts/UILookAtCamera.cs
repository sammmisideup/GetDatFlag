using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class UILookAtCamera : NetworkBehaviour
{
    [SerializeField] private Camera activeCam;
    void LateUpdate ()
    {
        SetCameraServerRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    private void SetCameraServerRpc()
    {
        SetCameraClientRpc();
    }

    [ClientRpc]
    private void SetCameraClientRpc()
    {
        Camera camera = Camera.main;
        transform.LookAt(transform.position + camera.transform.rotation * Vector3.forward, camera.transform.rotation * Vector3.up);
        activeCam = camera;
    }


}
