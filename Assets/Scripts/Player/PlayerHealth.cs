using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Entity
{
    //[SerializeField] float hitPoints = 100f;
    //private bool isDead = false;

    [Header("Sounds")]
    private AudioSource playerAudioPlayer;
    [SerializeField] private AudioClip deatClip;
    [SerializeField] private AudioClip hitClip;
    [SerializeField] private AudioClip itemPickUpClip;

    private void Awake()
    {
        playerAudioPlayer = GetComponent<AudioSource>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    public bool IsDead()
    {
        return isDead;
    }

    public void RestoreHealth(float newHealth)
    {
        if (isDead)
            return;

        currentHealth += newHealth;

    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        base.OnDamage(damage, hitPoint, hitNormal);

        if (!isDead)
        {
            playerAudioPlayer.PlayOneShot(hitClip);
            GetComponent<DisplayDamage>().ActiveDamageImpact();
        }
            
    }

    public override void Die()
    {
        base.Die();

        playerAudioPlayer.PlayOneShot(deatClip);
        //GetComponent<DeathHandler>().HandleDeath();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isDead)
        {
            IItem item = other.GetComponent<IItem>();

            if(item != null)
            {
                //item.Use(gameObject);
                playerAudioPlayer.PlayOneShot(itemPickUpClip);
            }
            
        }
        
    }
          
}
