using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class WeaponPickup : NetworkBehaviour
{
    [SerializeField] private GameObject weaponPunch;
    [SerializeField] private GameObject weaponClub;
    [SerializeField] private GameObject weaponHammer;
    [SerializeField] private Image hammerIcon;
    [SerializeField] private Image clubIcon;
        
    public WeaponCharge WeaponCharge;
    public AudioClip pickupSound;

    private void OnTriggerEnter(Collider col)
    {
        // if(!IsOwner) return;

        GameObject whatHit = col.gameObject; 

        if(whatHit.CompareTag("WeaponClub"))
        {
            GiveWeaponServerRpc(0);
            SendSoundServerRpc();
            WeaponCharge.clubCharges.Value = 10;
        }     

        if(whatHit.CompareTag("WeaponHammer"))
        {
            GiveWeaponServerRpc(1);
            SendSoundServerRpc();
            WeaponCharge.stunCharges.Value = 3;
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

    [ServerRpc(RequireOwnership = false)]
    private void GiveWeaponServerRpc(int code)
    {
        GiveWeaponClientRpc(code);
    }

    [ClientRpc]
    private void GiveWeaponClientRpc(int code)
    {
        if(code == 0)
        {
            // StartCoroutine(Club());
            SetClub();
        }

        if(code == 1)
        {
            // StartCoroutine(Hammer());
            SetHammer();
        }
    }

    private void SetClub()
    {
        Color32 clubColor = clubIcon.GetComponent<Image>().color;
        clubColor.a = 255;
        clubIcon.GetComponent<Image>().color = clubColor;

        Color32 hammerColor = hammerIcon.GetComponent<Image>().color;
        hammerColor.a = 50;
        hammerIcon.GetComponent<Image>().color = hammerColor;     

        weaponPunch.SetActive(false);
        weaponClub.SetActive(true);
        weaponHammer.SetActive(false);
        ChangeHairClientRpc(1);
    }

    private void SetHammer()
    {
        Color32 clubColor = clubIcon.GetComponent<Image>().color;
        clubColor.a = 50;
        clubIcon.GetComponent<Image>().color = clubColor;

        Color32 hammerColor = hammerIcon.GetComponent<Image>().color;
        hammerColor.a = 255;
        hammerIcon.GetComponent<Image>().color = hammerColor;        

        weaponPunch.SetActive(false);
        weaponClub.SetActive(false);
        weaponHammer.SetActive(true);
        ChangeHairClientRpc(2);        
    }
    IEnumerator Club()
    {
        weaponPunch.SetActive(false);
        weaponClub.SetActive(true);
        weaponHammer.SetActive(false);
        ChangeHairClientRpc(1);

        yield return new WaitForSeconds(10f);

        weaponPunch.SetActive(true);
        weaponClub.SetActive(false);
        weaponHammer.SetActive(false);
        ChangeHairClientRpc(0);
    }
    
    IEnumerator Hammer()
    {
        weaponPunch.SetActive(false);
        weaponClub.SetActive(false);
        weaponHammer.SetActive(true);
        ChangeHairClientRpc(2);

        yield return new WaitForSeconds(10f);

        weaponPunch.SetActive(true);
        weaponClub.SetActive(false);
        weaponHammer.SetActive(false);
            
        ChangeHairClientRpc(0);
    }    


    [ClientRpc]
    private void ChangeHairClientRpc(int value)
    {
        if(value == 0)
        {
            Debug.Log("Player#" + OwnerClientId + " acquired Punch!");            
        }

        if(value == 1)
        {
            Debug.Log("Player#" + OwnerClientId + " acquired a Club!");          
        }

        if(value == 2)
        {
            Debug.Log("Player#" + OwnerClientId + " acquired a Hammer!");        
        }

    }
}
