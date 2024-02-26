using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Powerup : NetworkBehaviour
{
    // public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnTriggerEnter(Collider col)
    {
        if(!IsOwner) return;

        GameObject whatHit = col.gameObject; 

        if(whatHit.CompareTag("speedBoost"))
        {
            // AudioClip clip = pickupSFX[UnityEngine.Random.Range(0, pickupSFX.Length)]; // AUDIO
            // audioSrc.PlayOneShot(clip);            

            this.GetComponent<PlayerController>().moveSpeed.Value += 10;
            // playerLives.text = "Lives \n" + Player.GetComponent<LivesRespawn>().lives; // TEXT UPDATE IF THERE'S ONE
            PowerupMessageServerRpc(0);
            // Destroy(whatHit);
        }

        if(whatHit.CompareTag("strengthBoost"))
        {
            // AudioClip clip = pickupSFX[UnityEngine.Random.Range(0, pickupSFX.Length)]; // AUDIO
            // audioSrc.PlayOneShot(clip);            

            this.GetComponent<PlayerController>().strength.Value += 10;
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
            Debug.Log("Client#" + OwnerClientId + " +10 Speed");
        }

        if(value == 1)
        {
            Debug.Log("Client#" + OwnerClientId + " +10 Strength");
        }
    }





}
