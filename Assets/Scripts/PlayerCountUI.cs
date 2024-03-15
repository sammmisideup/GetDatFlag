using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Netcode;

public class PlayerCountUI : NetworkBehaviour
{
    [SerializeField] private TextMeshProUGUI playersCountText;

    private NetworkVariable<int> playersNum = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone);


    // Update is called once per frame
    private void Update()
    {
        playersCountText.text = "Players: " + playersNum.Value.ToString();

        if(!IsServer) return;
        playersNum.Value = NetworkManager.Singleton.ConnectedClients.Count;

    }
}
