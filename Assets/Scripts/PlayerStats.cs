using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;

public class PlayerStats : NetworkBehaviour
{
    [SerializeField] private TextMeshProUGUI speedCount;
    [SerializeField] private TextMeshProUGUI knockbackStrength;
    [SerializeField] private TextMeshProUGUI ammo;

    [SerializeField] private GameObject playerController;
    [SerializeField] private GameObject hand2;

    void Update()
    {
        if(!IsOwner) return;
        
        speedCount.text = "Speed: " + playerController.GetComponent<PlayerController>().moveSpeed.Value.ToString();
        knockbackStrength.text = "Knockback Str: " + hand2.GetComponent<MeleeDetection>().knockbackForce.ToString();
        ammo.text = "Eggbomb Ammo: " + playerController.GetComponent<EggBombGun>().ammo.Value.ToString();      

    }

}
