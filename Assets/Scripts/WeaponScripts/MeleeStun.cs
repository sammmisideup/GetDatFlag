using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Services.Lobbies.Models;

public class MeleeStun : NetworkBehaviour
{
    public MeleeAttack MeleeAttack;
    // public GameObject hitParticle; --- PUT VFX PARTICLE HERE

    [SerializeField] private GameObject player;

    // public override void OnNetworkSpawn() --- you can just disable this
    // {
    //     base.OnNetworkSpawn();
    // }

    // void Start()
    // {
    //     var weaponHolder = this.transform.parent.gameObject;
    //     var playerObj = weaponHolder.transform.parent.gameObject;
    //     player = playerObj.transform.parent.gameObject;
    // }


    private void OnTriggerEnter(Collider other)
    {
        // if(IsClient)
        // {
            if(player.tag == "Team1" && other.tag == "Team2" && MeleeAttack.isAttacking)
            {
                // other.GetComponent<>().SetTrigger("Hit"); --- TRIGGER THE RECOIL ANIMATION FROM TARGET

                // Instantiate(hitParticle, new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z), other.transform.rotation); --- INSTANTIATE VFX PARTICLE

                other.GetComponent<PlayerController>().StunPlayer();


                Debug.Log("Team 1 stun");
            }

            if(player.tag == "Team2" && other.tag == "Team1" && MeleeAttack.isAttacking)
            {
                // other.GetComponent<>().SetTrigger("Hit"); --- TRIGGER THE RECOIL ANIMATION FROM TARGET

                // Instantiate(hitParticle, new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z), other.transform.rotation); --- INSTANTIATE VFX PARTICLE

                other.GetComponent<PlayerController>().StunPlayer();


                Debug.Log("Team 2 stun");
            }

            if(other.tag == "Dummy" && MeleeAttack.isAttacking)
            {
                // other.GetComponent<>().SetTrigger("Hit"); --- TRIGGER THE RECOIL ANIMATION FROM TARGET

                // Instantiate(hitParticle, new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z), other.transform.rotation); --- INSTANTIATE VFX PARTICLE

                other.GetComponent<PlayerController>().StunPlayer();


                Debug.Log("Dummy stunned");
            }

        // }
    }


}




