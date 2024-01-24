using System;
using UnityEngine;

public class Entity : MonoBehaviour, IDamageable
{
    public float startingHealth = 100f;
    public float currentHealth { get; protected set; }
    public bool isDead { get; protected set; }
    //public event Action onDeath;

    protected virtual void OnEnable()
    {
        isDead = false;
        currentHealth = startingHealth;
    }

    public virtual void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        Debug.Log(gameObject.name + "was Damaged!");
        currentHealth -= damage;

        if (currentHealth <= 0 && !isDead)
            Die();       

    }    

    public virtual void Die()
    {
        /*if (onDeath != null)
            onDeath();*/

        isDead = true;
    }
}
