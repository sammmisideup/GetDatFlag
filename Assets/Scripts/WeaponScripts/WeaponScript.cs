using System.Collections;
using UnityEngine;
using Unity.Netcode;

public class WeaponScript : NetworkBehaviour
{
    public float attackRange = 2f;
    public float knockbackForce = 5f;

    public KeyCode attackKey = KeyCode.G;
    public KeyCode dropKey = KeyCode.F;
    
    public Transform weaponTransform;

    // [ServerRpc]
    // public void PerformAttackServerRpc()
    // {
    //     PerformAttackClientRpc();
    // }

    void Update()
    {
        if (!IsOwner) return;

            if (Input.GetKeyDown(attackKey))
            {
                PerformAttackServerRpc();
                Debug.Log("Attack");
            }

            if (Input.GetKeyDown(dropKey))
            {
                DropWeaponServerRpc();
                Debug.Log("Drop");
            }            
    }

    [ServerRpc]
    public void PerformAttackServerRpc()
    {
        if (!IsOwner) return;
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange);

            foreach (Collider collider in colliders)
            {
                ApplyKnockback(collider.gameObject);
            }
        }
    }

    void ApplyKnockback(GameObject target)
    {
        if (target.TryGetComponent<Rigidbody>(out Rigidbody targetRigidbody))
        {
            Vector3 knockbackDirection = (target.transform.position - transform.position).normalized;
            targetRigidbody.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
            Debug.Log("Knock");
        }
    }


    [ServerRpc]
    public void DropWeaponServerRpc()
    {    
        weaponTransform.transform.parent = null;
        weaponTransform.GetComponent<Rigidbody>().isKinematic = false;
        weaponTransform.GetComponent<Collider>().enabled = true;
        weaponTransform.GetComponent<WeaponScript>().enabled = false;
    }

}
