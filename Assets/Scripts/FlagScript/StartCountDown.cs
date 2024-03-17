using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;


public class StartCountDown : NetworkBehaviour
{
    CountDown countDown;


    private GameObject flagClone;   

    // Start is called before the first frame update
    void Start()
    {
        countDown = gameObject.GetComponent<CountDown>();
    }

    void Update()
    {
        flagClone = GameObject.Find("Flag(Clone)");

        if (flagClone == null)
        {
            countDown.enabled = false;
            countDown.timeRemaining = 10f;
            countDown.timer.fillAmount = countDown.timeRemaining / countDown.maxTime.Value;
        }   
    }

    private void OnTriggerEnter(Collider other){

        if ((countDown.player.CompareTag("Team1") || countDown.player.CompareTag("Team2")) && other.gameObject.tag == "Flag")
        {
            countDown.enabled = true;
            Debug.Log("Enter Flag");
        }
    } 
    
    private void OnTriggerExit(Collider other){
        
        if ((countDown.player.CompareTag("Team1") || countDown.player.CompareTag("Team2")) && other.gameObject.tag == "Flag")
        {
            countDown.enabled = false;
            Debug.Log("Exit Flag");
        }
    } 




}
