using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Powerup : NetworkBehaviour
{
    public NetworkVariable<float> buffedMovespeed = new NetworkVariable<float>(5, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public NetworkVariable<float> buffedKnockback = new NetworkVariable<float>(5, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

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

            playerController.GetComponent<PlayerController>().moveSpeed.Value += buffedMovespeed.Value;

            // playerLives.text = "Lives \n" + Player.GetComponent<LivesRespawn>().lives; // TEXT UPDATE IF THERE'S ONE
            PowerupMessageServerRpc(0);
            // Destroy(whatHit);
        }

        if(whatHit.CompareTag("strengthBoost"))
        {
            // AudioClip clip = pickupSFX[UnityEngine.Random.Range(0, pickupSFX.Length)]; // AUDIO
            // audioSrc.PlayOneShot(clip);            

            hand2.GetComponent<MeleeDetection>().knockbackForce += buffedKnockback.Value;
            
            // playerLives.text = "Lives \n" + Player.GetComponent<LivesRespawn>().lives; // TEXT UPDATE IF THERE'S ONE
            PowerupMessageServerRpc(1);
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
            Debug.Log("Client#" + OwnerClientId + " +Club Weapon");
        }

    }





}
