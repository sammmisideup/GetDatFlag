using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFlag : MonoBehaviour
{
    public float timeRemaining;
    public float maxTime = 5.0f;

    void Start()
    {
        timeRemaining = maxTime;

    }

    void Update()
    {
        if(timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }

        if(timeRemaining < 0)
        {
            Invoke("ResetTime", 2f);
        }
    }

    private void OnTriggerStay(Collider col)
    {
        GameObject whatHit = col.gameObject;
        if (whatHit.CompareTag("flag") && timeRemaining  <= 0)
        {
            Destroy (whatHit);
            Debug.Log("Flag Destroy");
        }
    }

    private void ResetTime()
    {
        maxTime = 15f;
        timeRemaining = maxTime;
    }

}
