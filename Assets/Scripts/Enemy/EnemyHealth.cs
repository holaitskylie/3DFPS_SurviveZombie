using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float hitPoints = 100f;
    private Animator animator;
    private bool isDead = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public bool IsDead()
    {
        return isDead;
    }

    public void TakeDamage(float damage)
    {
        //GetComponent<EnemyAI>().OnDamaged();
        BroadcastMessage("OnDamaged");
        hitPoints -= damage;

        if(hitPoints <= 0)
        {
            hitPoints = 0;
            Die();
        }
    }


    private void Die()
    {
        if (isDead) return;

        isDead = true;
        animator.SetTrigger("die");
    }
}
