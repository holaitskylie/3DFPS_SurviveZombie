using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeBarrel : MonoBehaviour, IDamageable
{
    [SerializeField] private float barrelHealth = 10f;
    [SerializeField] private float barrelDamage = 10f;

    [Header("Explosion")]
    [SerializeField] LayerMask whatIsEnemy;
    [SerializeField] private float explosionRadius = 20f;
    [SerializeField] private ParticleSystem explosionEffect;
    //private AudioSource explosionAudio;
    //[SerializeField] private AudioClip explosionClip;

    private void Awake()
    {
        //explosionAudio = GetComponent<AudioSource>();
    }
    public void OnDamage(float damage, Vector3 hitPoint, Vector3 hitnormal)
    {
        barrelHealth -= damage;

        if (barrelHealth <= 0)
        {
            PlayExplosionEffect();
            Explode(barrelDamage, hitPoint, hitnormal);

        }
        
    }

    private void Explode(float damage, Vector3 hitPoint, Vector3 hitnormal)
    {
        //enemy 데미지 피해
        Collider[] colliders = Physics.OverlapSphere(hitPoint, explosionRadius, whatIsEnemy);
        for (int i = 0; i < colliders.Length; i++)
        {
            EnemyHealth enemy = colliders[i].GetComponent<EnemyHealth>();

            if (enemy != null)
            {
                enemy.OnDamage(damage, hitPoint, hitnormal);
            }

        }

        Destroy(gameObject);
    }

    private void PlayExplosionEffect()
    {
        //폭발 이펙트 재생
        //explosionAudio.PlayOneShot(explosionClip);
        explosionEffect.transform.parent = null;

        explosionEffect.Play();
        Destroy(explosionEffect.gameObject, 5f);
    }
}
