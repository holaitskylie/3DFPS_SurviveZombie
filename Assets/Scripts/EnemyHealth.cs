using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float hitPoints = 100f;

    public void TakeDamage(float damage)
    {
        //GetComponent<EnemyAI>().OnDamaged();
        BroadcastMessage("OnDamaged");
        hitPoints -= damage;

        if(hitPoints <= 0)
        {
            hitPoints = 0;
            Destroy(gameObject);
        }
    }
}
