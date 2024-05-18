using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkAudio : MonoBehaviour
{
    public PlayerController PlayerController;
    public AudioSource srcWalk;

    void Update()
    {
        if((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) && PlayerController.grounded)
        {
            srcWalk.enabled = true;
        }
        
        else if(PlayerController.grounded == false)
        {
           srcWalk.enabled = false; 
        }

        else
        {
            srcWalk.enabled = false;
        } 
    }
}
