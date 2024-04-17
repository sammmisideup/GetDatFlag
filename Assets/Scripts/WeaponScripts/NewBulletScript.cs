using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NewBulletScript : NetworkBehaviour
{
    [SerializeField]
    private float speed = 20f;

    [SerializeField]
    private GameObject player;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        GetComponent<Rigidbody>().velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        Destroy(gameObject);
    }
}
