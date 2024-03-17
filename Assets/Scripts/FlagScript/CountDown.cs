using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class CountDown : NetworkBehaviour
{
    public static CountDown instance;

    public NetworkVariable<float> maxTime = new NetworkVariable<float>(10, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public Image timer;
    float timeRemaining;
    //public float maxTime.Value = 10.0f;
    public GameObject youWon;
    public float roundDelay = 2f;

    CountDown countDown;
    
    void Awake() {
        if (instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        timeRemaining = maxTime.Value;
        countDown = gameObject.GetComponent<CountDown>();
    }

    // Update is called once per frame
    void Update()
    { 
       

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            timer.fillAmount = timeRemaining / maxTime.Value;
        }
        if (timeRemaining < 0)
        {
            
            Invoke("TimerMax", 2f);
            Invoke("YouWonOn", 0f);
            Invoke("YouWonOff", roundDelay);
            
            DestroyFlag.instance.enabled = true;
            countDown.enabled = false;
            Debug.Log("Flag Destroy");
            Debug.Log(timeRemaining);
            
            
        }
        
    }
    
    public void TimerMax()
    {
        maxTime.Value = 10f;
        timeRemaining = maxTime.Value;
        timeRemaining += Time.deltaTime;
        timeRemaining = Mathf.Clamp(timeRemaining, 0, maxTime.Value);
        timer.fillAmount = timeRemaining / maxTime.Value;
        

    }

    
    private void YouWonOn()
    {
        
        youWon.SetActive(true);
        Debug.Log("Canvas");
        Invoke("NewFlag", 0f);
    }

    
    private void YouWonOff()
    {
         
        youWon.SetActive(false);
        Debug.Log("Destroy Canvas");
    }

    private void NewFlag() {
        FlagSpawner.instance.Start();
    } 
    

    
}
