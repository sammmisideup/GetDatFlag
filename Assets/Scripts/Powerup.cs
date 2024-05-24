using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class Powerup : NetworkBehaviour
{
    public NetworkVariable<float> buffedMovespeed = new NetworkVariable<float>(6, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public NetworkVariable<float> buffedKnockback = new NetworkVariable<float>(60, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    [SerializeField] private GameObject playerController;
    [SerializeField] private GameObject hand2;
    [SerializeField] private Image speedIcon;
    [SerializeField] private Image strengthIcon;
    public AudioClip pickupSound;    

    private void OnTriggerEnter(Collider col)
    {
        if(!IsOwner) return;

        GameObject whatHit = col.gameObject; 

        if(whatHit.CompareTag("speedBoost"))
        {
            SendSoundServerRpc();
            StartCoroutine(SpeedBoost());

            PowerupMessageServerRpc(0);
        }

        if(whatHit.CompareTag("strengthBoost"))
        {
            SendSoundServerRpc();
            StartCoroutine(KnockbackBoost());
            
            PowerupMessageServerRpc(1);
        }

        if(whatHit.CompareTag("Egg"))
        {
            SendSoundServerRpc();
            playerController.GetComponent<EggBombGun>().ammo.Value = 5;
            
            PowerupMessageServerRpc(2);
        }      

        if(whatHit.CompareTag("EggBomb"))
        {
            SendSoundServerRpc();
            StartCoroutine(SlowDown());
            
            PowerupMessageServerRpc(3);
        }          
    

    }

    [ServerRpc(RequireOwnership = false)]
    private void SendSoundServerRpc()
    {
        SendSoundClientRpc();
    }

    [ClientRpc]
    private void SendSoundClientRpc()
    {
        float volume = 1f;
        AudioSource.PlayClipAtPoint(pickupSound, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), volume);
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
        Color32 speedColor = speedIcon.GetComponent<Image>().color;
        speedColor.a = 255;
        speedIcon.GetComponent<Image>().color = speedColor;        
        playerController.GetComponent<PlayerController>().moveSpeed.Value += buffedMovespeed.Value;

        yield return new WaitForSeconds(5f);

        Color32 speedColor2 = speedIcon.GetComponent<Image>().color;
        speedColor2.a = 50;
        speedIcon.GetComponent<Image>().color = speedColor2; 
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
        Color32 strColor = strengthIcon.GetComponent<Image>().color;
        strColor.a = 255;
        strengthIcon.GetComponent<Image>().color = strColor;  
        hand2.GetComponent<MeleeDetection>().knockbackStrength.Value += buffedKnockback.Value;

        yield return new WaitForSeconds(6f);

        Color32 strColor2 = strengthIcon.GetComponent<Image>().color;
        strColor2.a = 50;
        strengthIcon.GetComponent<Image>().color = strColor2; 

        hand2.GetComponent<MeleeDetection>().knockbackStrength.Value = 65;
    } 



}
