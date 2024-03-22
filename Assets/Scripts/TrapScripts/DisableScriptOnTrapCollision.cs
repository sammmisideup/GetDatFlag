using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class DisableScriptOnTrapCollision : NetworkBehaviour
{
    public string scriptToDisableTag = "Trap"; 
    public MonoBehaviour scriptToDisable; 
    public float disableDuration = 3f; 

    private bool isScriptDisabled = false; 
    private float disableTimer = 0f; 

    private void OnCollisionEnter(Collision collision)
    {
        if (IsLocalPlayer && collision.gameObject.CompareTag(scriptToDisableTag))
        {
            if (!isScriptDisabled)
            {
                DisableScript();
            }
        }
    }

    private void Update()
    {
        if (isScriptDisabled)
        {
            disableTimer += Time.deltaTime;
            if (disableTimer >= disableDuration)
            {
                EnableScript();
            }
        }
    }

    private void DisableScript()
    {
        if (scriptToDisable != null)
        {
            scriptToDisable.enabled = false;
            isScriptDisabled = true;
            disableTimer = 0f;
        }
    }

    private void EnableScript()
    {
        if (scriptToDisable != null)
        {
            scriptToDisable.enabled = true;
            isScriptDisabled = false;
        }
    }
}
