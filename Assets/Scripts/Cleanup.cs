using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Cleanup : MonoBehaviour
{

    void Start()
    {
        CleanupFunction();
    }

    void CleanupFunction()
    {
        if (NetworkManager.Singleton != null)
        {
            Destroy(NetworkManager.Singleton.gameObject);
        }
    }

}
