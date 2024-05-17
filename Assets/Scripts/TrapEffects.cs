using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class TrapEffects : NetworkBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private PlayerController PlayerController;

    void OnTriggerEnter(Collider collision) // Put Slow in OnTriggerStay, Keep Stun in OnTriggerEnter; convert the traps into Trigger
    {
        if (!IsOwner) return;

        // if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Team1") || collision.gameObject.CompareTag("Team2"))
        // {
        //     BounceBack();
        // }
        if (collision.gameObject.CompareTag("StickTrap"))
        {
            StickToTrap(); 
        }
    }

    void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.CompareTag("SlowTrap"))
        {
            SlowPlayer();
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("SlowTrap"))
        {
            UnslowPlayer();
        }
    }

    // void BounceBack()
    // {
    //     if (!IsOwner) return;
    //     //bounce logic
    //     Vector3 bounceDirection = -rb.velocity.normalized;

    //     rb.AddForce(bounceDirection * bounceForce, ForceMode.Impulse);

    //     Debug.Log("Player Bounced Back!");

    // }

    public void SlowPlayer()
    {
        if(!IsOwner) return;
   
        PlayerController.moveSpeed.Value = 4f;
        Debug.Log("Player Slowed Down!");    
    }

    public void UnslowPlayer()
    {
        if(!IsOwner) return;
   
        StartCoroutine(RestorePlayerSpeed());
    }    

    IEnumerator RestorePlayerSpeed()
    {
        // reduce player speeddd
        PlayerController.moveSpeed.Value = 4f;
        Debug.Log("Player Slowed Down!");    

        yield return new WaitForSeconds(1f);

        // restore player speedd
        PlayerController.moveSpeed.Value = 8f; //timer
        Debug.Log("Player Speed Restored!");
    }



    void StickToTrap()
    {
    if (!IsOwner) return;
    Debug.Log("Player Stuck to Trap!");

    // freeze player after collision
    rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;

    StartCoroutine(UnstickFromTrap());
    }  

    IEnumerator UnstickFromTrap()
    {
        yield return new WaitForSeconds(2f);

        // unfreeze si player
        rb.constraints = RigidbodyConstraints.None;

        // ikikeep yung rotation constraint ni player
        rb.freezeRotation = true;

        Debug.Log("Player Unstuck from Trap!");
    }
}
