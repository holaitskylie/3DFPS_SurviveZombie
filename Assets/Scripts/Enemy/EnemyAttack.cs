using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    PlayerHealth target;
    public float damage = 40f;
    void Start()
    {
        target = FindObjectOfType<PlayerHealth>();
        
    }

    public void AttackHitEvent()
    {
        if (target != null)
        {
            Debug.Log("Attack!!");
            //target.TakeDamage(damage);
            Vector3 hitPoint = target.transform.position;
            Vector3 hitNormal = transform.position - target.transform.position; 
            target.OnDamage(damage, hitPoint, hitNormal);

            //target.GetComponent<DisplayDamage>().ActiveDamageImpact();

        }
        else
            return;
    }
}
