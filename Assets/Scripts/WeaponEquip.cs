using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class WeaponEquip : NetworkBehaviour
{
    public Transform handTransform;
    public GameObject currentWeapon;
    public Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        if (IsLocalPlayer && other.CompareTag("Weapon"))
        {
            if (currentWeapon == null)
            {
                EquipWeapon(other.gameObject);
            }
        }
    }

    private void Update()
    {
        if (IsLocalPlayer)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                DropWeapon();
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                Attack();
            }
        }
    }

    private void EquipWeapon(GameObject weapon)
    {
        currentWeapon = weapon;
        currentWeapon.transform.SetParent(handTransform);
        currentWeapon.transform.localPosition = Vector3.zero;
        currentWeapon.transform.localRotation = Quaternion.identity;
        Rigidbody weaponRigidbody = currentWeapon.GetComponent<Rigidbody>();
        if (weaponRigidbody != null)
        {
            weaponRigidbody.isKinematic = true;
        }
    }

    private void DropWeapon()
    {
        if (currentWeapon != null)
        {
            Rigidbody weaponRigidbody = currentWeapon.GetComponent<Rigidbody>();
            if (weaponRigidbody != null)
            {
                weaponRigidbody.isKinematic = false;
            }
            currentWeapon.transform.SetParent(null);
            currentWeapon = null;
        }
    }

    private void Attack()
    {
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }
        Debug.Log("Attacking!");
    }
}
