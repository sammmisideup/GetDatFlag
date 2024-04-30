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

    public float timeRemaining;
    //public float maxTime.Value = 10.0f;

    public Image timer;
    public GameObject youWon;
    public float roundDelay = 2f;

    CountDown countDown;

    public GameObject player;      
    
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
        if ((player.CompareTag("Team1")|| player.CompareTag("Team2")) && timeRemaining <= 0)
        { 
            Invoke("TimerMax", 2f);
            YouWonOn();

            
            countDown.enabled = false;
            Debug.Log("Flag Destroy");
            Debug.Log(timeRemaining);

        // to add score
        if(player.CompareTag("Team1"))
        {
                TeamScore.team1ScoreNew.Value ++; 

                // AddScoreServerRpc(0);
                Debug.Log("Team1 +1 Score!");
        }

        if(player.CompareTag("Team2"))
        {
                TeamScore.team2ScoreNew.Value ++; 

                // AddScoreServerRpc(1);
                Debug.Log("Team2 +1 Score!");
        }
        }
        
    }

    [ServerRpc]
    private void AddScoreServerRpc(int value)
    {
        if(value == 0)
        {
            // TeamScore.team1Score.Value += 1;          
            TeamScore.team2ScoreNew.Value += 1;
        }

        if(value == 1)
        {
            // TeamScore.team2Score.Value += 1; 
            TeamScore.team2ScoreNew.Value += 1;
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
        
        youWon.SetActive(true);                        // CURRENT ISSUE IS THE HOST SCORE NOTIF DOESN'T SHOW UP ON THE CLIENT; BUT CLIENT'S SCORE SHOWS UP ON HIS SCREEN
        Debug.Log("Player " + OwnerClientId + "Won");
        FlagSpawner.instance.Start();

        Invoke("YouWonOff", roundDelay);     
    }

    
    private void YouWonOff()
    {
         
        youWon.SetActive(false);
        Debug.Log("Destroy Canvas");
    }

    private void NewFlag() {
        FlagSpawner.instance.Start();
    } 

    private void OnTriggerStay(Collider col)
    {
        GameObject whatHit = col.gameObject;

        if(timeRemaining < 0 && whatHit.CompareTag("Flag"))
        {
            DestroyFlag.instance.enabled = true;          
        }


    }
    

    
}
