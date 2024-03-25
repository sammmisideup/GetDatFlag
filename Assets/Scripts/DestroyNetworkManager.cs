using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyNetworkManager : MonoBehaviour
{
    public static DestroyNetworkManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
