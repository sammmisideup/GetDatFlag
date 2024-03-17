using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class StartCountDown : NetworkBehaviour
{
    CountDown countDown;

    // Start is called before the first frame update
    void Start()
    {
        countDown = gameObject.GetComponent<CountDown>();
    }


    private void OnTriggerEnter(Collider other){

        if (other.gameObject.tag == "Flag")
        {
            countDown.enabled = true;
            Debug.Log("Enter Flag");
        }
    } 
    
    private void OnTriggerExit(Collider other){
        
        if (other.gameObject.tag == "Flag")
        {
            countDown.enabled = false;
            Debug.Log("Exit Flag");
        }
    } 




}
