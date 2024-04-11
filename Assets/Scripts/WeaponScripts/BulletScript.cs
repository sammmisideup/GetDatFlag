using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class BulletScript : NetworkBehaviour
{
    [SerializeField]
    private float speed = 20f;

    [SerializeField]
    private float knockbackForce;    

    [SerializeField] private GameObject player;


    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        GetComponent<Rigidbody>().velocity = this.transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.tag == "Team2")
        {
            // other.GetComponent<>().SetTrigger("Hit"); --- TRIGGER THE RECOIL ANIMATION FROM TARGET

            // Instantiate(hitParticle, new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z), other.transform.rotation); --- INSTANTIATE VFX PARTICLE

            Vector3 knockbackDirection = (other.transform.position - transform.position).normalized;
            other.GetComponent<Rigidbody>().AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
            Destroy(this);


            Debug.Log("Team1 Player " + OwnerClientId+  " EggBomb hit " + other.tag);
        }

        if(other.tag == "Team1")
        {
            // other.GetComponent<>().SetTrigger("Hit"); --- TRIGGER THE RECOIL ANIMATION FROM TARGET

            // Instantiate(hitParticle, new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z), other.transform.rotation); --- INSTANTIATE VFX PARTICLE

            Vector3 knockbackDirection = (other.transform.position - transform.position).normalized;
            other.GetComponent<Rigidbody>().AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
            Destroy(this);

            Debug.Log("Team2 Player " + OwnerClientId+  " EggBomb hit " + other.tag);
        }

        if(other.tag == "Dummy")
        {
            // other.GetComponent<>().SetTrigger("Hit"); --- TRIGGER THE RECOIL ANIMATION FROM TARGET

            // Instantiate(hitParticle, new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z), other.transform.rotation); --- INSTANTIATE VFX PARTICLE

            Vector3 knockbackDirection = (other.transform.position - transform.position).normalized;
            other.GetComponent<Rigidbody>().AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
            Destroy(this);

            Debug.Log("Player" + OwnerClientId+  " EggBomb hit " + other.tag);
        }
    }



}
