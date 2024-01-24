using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    PlayerHealth target;
    [SerializeField] private float damage = 40f;
    void Start()
    {
        target = FindObjectOfType<PlayerHealth>();
        
    }

    public void AttackHitEvent()
    {
        if (target != null)
        {
            Debug.Log("Attack!!");
            target.TakeDamage(damage);
            target.GetComponent<DisplayDamage>().ActiveDamageImpact();

        }
        else
            return;
    }
}
