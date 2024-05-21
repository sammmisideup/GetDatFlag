using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;
using Unity.Collections;
using System;

public class PlayerName : NetworkBehaviour
{
    [SerializeField] private TextMeshProUGUI playerNameIn;
    // private NetworkVariable<FixedString128Bytes> networkPlayerName = new NetworkVariable<FixedString128Bytes>(
    //     "Player: 0", NetworkVariableReadPermission.Everyone,NetworkVariableWritePermission.Server);

    private NetworkVariable<FixedString32Bytes> playerName = new NetworkVariable<FixedString32Bytes>("", NetworkVariableReadPermission.Everyone,NetworkVariableWritePermission.Owner);

    public override void OnNetworkSpawn()
    {
        // networkPlayerName.Value = "Player: " + (OwnerClientId + 1);
        // playerNameIn.text = networkPlayerName.Value.ToString();

        base.OnNetworkSpawn();
        playerName.OnValueChanged += OnValueChanged;
    }

    private void OnValueChanged(FixedString32Bytes previousValue, FixedString32Bytes newValue)
    {
        if (previousValue != newValue)
        {
            Debug.Log("Name changed");
        }
    }

    void Update()
    {
        if(!IsOwner) return;
        if(Input.GetKey(KeyCode.M))
        {
            playerName.Value = "testing " + OwnerClientId;
            OnNameChangedServerRpc(playerName.Value);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void OnNameChangedServerRpc(FixedString32Bytes newValue)
    {
        OnNameChangedClientRpc(newValue);
    }

    [ClientRpc]
    private void OnNameChangedClientRpc(FixedString32Bytes newValue)
    {
        playerName.Value = newValue;
        playerNameIn.text = playerName.Value.ToString();
        Debug.Log("Client #" + OwnerClientId + " changed name");
    }
}