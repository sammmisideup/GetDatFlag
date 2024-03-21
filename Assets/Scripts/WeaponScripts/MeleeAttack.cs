using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class MeleeAttack : NetworkBehaviour
{

    [SerializeField]
    // private GameObject meleeWeapon;
    public bool canAttack = true;
    public float attackCooldown = 1f;
    public bool isAttacking = false;

    // public AudioClip attackSFX; ------ ENABLE WHEN YOU HAVE SOUND EFFECTS



    void Update()
    {
        if(Input.GetMouseButtonDown(0) && IsOwner)
        {
            if(canAttack)
            {
                WeaponAttackServerRpc();
            }
        }
    }


    [ServerRpc(RequireOwnership = false)]
    private void WeaponAttackServerRpc()
    {
        // if(IsClient)
        // {
            isAttacking = true;
            canAttack = false;
            // Animator anim = meleeWeapon.GetComponent<Animator>();
            // anim.SetTrigger("Attack");

            // AudioSource ac = GetComponent<AudioSource>(); -- FOR THE AUDIO SOURCE
            // ac.PlayOneShot(attackSFX); -- PLAYING THE AUDIO SOURCE

            StartCoroutine(ResetAttackCooldown());             
        // }
    }

    IEnumerator ResetAttackCooldown()
    {
        StartCoroutine(ResetAttackBool());   
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

        IEnumerator ResetAttackBool()
    {
        yield return new WaitForSeconds(1f);
        isAttacking = false;
    }


}
