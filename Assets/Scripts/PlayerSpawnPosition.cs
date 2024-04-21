using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerSpawnPosition : NetworkBehaviour
{
    [SerializeField] private float positionRange;
    [SerializeField] private GameObject player;
    // [SerializeField] private GameObject timerObj;
    // [SerializeField] private Timer timer;

    public override void OnNetworkSpawn()
    {
        SpawnPositionServerRpc();
    }

    // void Update()
    // {
    //     if(timer.timeValue.Value == 300)
    //     {
    //         transform.position = new Vector3(-58f, -2.5f, Random.Range(-20f, -30f));
    //         transform.rotation = new Quaternion(0f, 0f, 0f, 0f);            
    //     }
    // }    

    [ServerRpc(RequireOwnership = false)]
    private void SpawnPositionServerRpc()
    {
        player.transform.position = new Vector3(Random.Range(-72f, -90f), Random.Range(16f, 16f), Random.Range(-6f, -28f));
        player.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
    }

    // IEnumerator CheckTimer()
    // {
    //     yield return new WaitForSeconds(0.475f);
    //     timerObj = GameObject.Find("TimerCanvas");
    //     timer = timerObj.GetComponent<Timer>();   
        
    // }    

    // [ServerRpc(RequireOwnership = false)]
    // private void GameStartPositionServerRpc()
    // {

    // }


}
