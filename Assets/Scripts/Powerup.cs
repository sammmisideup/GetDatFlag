using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Powerup : NetworkBehaviour
{
    public NetworkVariable<float> buffedMovespeed = new NetworkVariable<float>(6, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public NetworkVariable<float> buffedKnockback = new NetworkVariable<float>(35, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    [SerializeField] private GameObject playerController;
    [SerializeField] private GameObject hand2;

    private void OnTriggerEnter(Collider col)
    {
        if(!IsOwner) return;

        GameObject whatHit = col.gameObject; 

        if(whatHit.CompareTag("speedBoost"))
        {
            // AudioClip clip = pickupSFX[UnityEngine.Random.Range(0, pickupSFX.Length)]; // AUDIO
            // audioSrc.PlayOneShot(clip);            

            StartCoroutine(SpeedBoost());

            // playerLives.text = "Lives \n" + Player.GetComponent<LivesRespawn>().lives; // TEXT UPDATE IF THERE'S ONE
            PowerupMessageServerRpc(0);
            // Destroy(whatHit);
        }

        if(whatHit.CompareTag("strengthBoost"))
        {
            // AudioClip clip = pickupSFX[UnityEngine.Random.Range(0, pickupSFX.Length)]; // AUDIO
            // audioSrc.PlayOneShot(clip);            

            StartCoroutine(KnockbackBoost());
            
            // playerLives.text = "Lives \n" + Player.GetComponent<LivesRespawn>().lives; // TEXT UPDATE IF THERE'S ONE
            PowerupMessageServerRpc(1);
            // Destroy(whatHit);
        }

        if(whatHit.CompareTag("Egg"))
        {
            // AudioClip clip = pickupSFX[UnityEngine.Random.Range(0, pickupSFX.Length)]; // AUDIO
            // audioSrc.PlayOneShot(clip);            

            playerController.GetComponent<EggBombGun>().ammo.Value = 5;
            
            
            // playerLives.text = "Lives \n" + Player.GetComponent<LivesRespawn>().lives; // TEXT UPDATE IF THERE'S ONE
            PowerupMessageServerRpc(2);
            // Destroy(whatHit);
        }      

        if(whatHit.CompareTag("EggBomb"))
        {
            // AudioClip clip = pickupSFX[UnityEngine.Random.Range(0, pickupSFX.Length)]; // AUDIO
            // audioSrc.PlayOneShot(clip);            

            StartCoroutine(SlowDown());
            
            // playerLives.text = "Lives \n" + Player.GetComponent<LivesRespawn>().lives; // TEXT UPDATE IF THERE'S ONE
            PowerupMessageServerRpc(3);
            // Destroy(whatHit);
        }          
    

    }

    [ServerRpc]
    private void PowerupMessageServerRpc(int value)
    {
        if(value == 0)
        {
            Debug.Log("Client#" + OwnerClientId + " +5 Speed");
        }

        if(value == 1)
        {
            Debug.Log("Client#" + OwnerClientId + " +5 Strength");
        }

        if(value == 2)
        {
            Debug.Log("Client#" + OwnerClientId + " + 5 Bullets");
        }

        if(value == 3)
        {
            Debug.Log("Client#" + OwnerClientId + " + Slowed Down");
        }        

    }

    private void AddAmmo()
    {
        playerController.GetComponent<EggBombGun>().ammo.Value = 5;
    }
    IEnumerator SpeedBoost()
    {
        playerController.GetComponent<PlayerController>().moveSpeed.Value += buffedMovespeed.Value;
        yield return new WaitForSeconds(5f);
        playerController.GetComponent<PlayerController>().moveSpeed.Value = 8f;
    }

    IEnumerator SlowDown()
    {
        playerController.GetComponent<PlayerController>().moveSpeed.Value = 4f;
        yield return new WaitForSeconds(1.5f);
        playerController.GetComponent<PlayerController>().moveSpeed.Value = 8f;
    }    

    IEnumerator KnockbackBoost()
    {
        hand2.GetComponent<MeleeDetection>().knockbackForce += buffedKnockback.Value;
        yield return new WaitForSeconds(6f);
        hand2.GetComponent<MeleeDetection>().knockbackForce = 65;
    } 



}
