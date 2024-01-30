using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class PlayerHealth : Entity
{
    private FirstPersonController controller;
    [SerializeField] private GameObject weapon;
    [SerializeField] private Animator animator;
    [SerializeField] private CameraManager cameraManager;

    [Header("Sounds")]
    private AudioSource playerAudioPlayer;
    [SerializeField] private AudioClip deatClip;
    [SerializeField] private AudioClip hitClip;
    [SerializeField] private AudioClip itemPickUpClip;

    private void Awake()
    {
        playerAudioPlayer = GetComponent<AudioSource>();
        controller = GetComponent<FirstPersonController>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        controller.enabled = true;
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
        controller.enabled = false;
        //GetComponent<DeathHandler>().HandleDeath();

        animator.SetTrigger("Die");
        weapon.gameObject.SetActive(false);
        cameraManager.SwitchCamera(cameraManager.deadCam);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isDead)
        {
            IItem item = other.GetComponent<IItem>();

            if(item != null)
            {
                item.Use(gameObject);
                playerAudioPlayer.PlayOneShot(itemPickUpClip);
            }
            
        }
        
    }
          
}
