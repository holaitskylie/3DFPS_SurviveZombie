using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;
    
    private bool isDead = false;

    public bool IsDead()
    {
        return isDead;
    }
    
    public void TakeDamage(float damage)
    {
        hitPoints -= damage;
        if (hitPoints <= 0)
        {
            hitPoints = 0;
            Debug.Log("You died");
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return;

        isDead = true;
        GetComponent<DeathHandler>().HandleDeath();
    }
}
