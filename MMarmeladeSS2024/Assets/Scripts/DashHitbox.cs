using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashHitbox : MonoBehaviour
{
    PlayerMovement parent;

    bool hitEnemy = false;

    void Awake()
    {
        parent = gameObject.GetComponentInParent<PlayerMovement>();
    }

    void OnTriggerStay(Collider other)
    {
        CheckTrigger(other);
    }

    void CheckTrigger(Collider other)
    {
        if (other.CompareTag("DashHitbox") && parent.isDashing && !hitEnemy)
        {
            hitEnemy = true;
            if(other.gameObject.GetComponentInParent<PlayerMovement>().isDashing)
            {
                Debug.Log("Colliding with enemy who is also dashing");
                other.gameObject.GetComponentInParent<PlayerMovement>().isDashing = false;
                parent.isDashing = false;
                StartCoroutine(parent.KnockbackCoroutine());
                StartCoroutine(other.gameObject.GetComponentInParent<PlayerMovement>().KnockbackCoroutine());
                hitEnemy = false;
            }
            else
            {
                Debug.Log("Colliding with enemy");
                parent.isDashing = false;
                other.gameObject.GetComponentInParent<PlayerMovement>().ReceiveDamage();
                hitEnemy = false;
            }
        }
    }
}
