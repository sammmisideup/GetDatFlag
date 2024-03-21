using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class MeleeDetection : NetworkBehaviour
{
    public MeleeAttack MeleeAttack;
    // public GameObject hitParticle; --- PUT VFX PARTICLE HERE
    [SerializeField]
    public float knockbackForce;

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

                Vector3 knockbackDirection = (other.transform.position - transform.position).normalized;
                other.GetComponent<Rigidbody>().AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);


                Debug.Log("Team 1 attack");
            }

            if(player.tag == "Team2" && other.tag == "Team1" && MeleeAttack.isAttacking)
            {
                // other.GetComponent<>().SetTrigger("Hit"); --- TRIGGER THE RECOIL ANIMATION FROM TARGET

                // Instantiate(hitParticle, new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z), other.transform.rotation); --- INSTANTIATE VFX PARTICLE

                Vector3 knockbackDirection = (other.transform.position - transform.position).normalized;
                other.GetComponent<Rigidbody>().AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);


                Debug.Log("Team 2 attack");
            }

            if(other.tag == "Dummy" && MeleeAttack.isAttacking)
            {
                // other.GetComponent<>().SetTrigger("Hit"); --- TRIGGER THE RECOIL ANIMATION FROM TARGET

                // Instantiate(hitParticle, new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z), other.transform.rotation); --- INSTANTIATE VFX PARTICLE

                Vector3 knockbackDirection = (other.transform.position - transform.position).normalized;
                other.GetComponent<Rigidbody>().AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);


                Debug.Log("Dummy attacked");
            }

        // }
    }


}




