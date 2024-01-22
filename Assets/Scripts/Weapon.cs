using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPSCam;
    [SerializeField] private float range = 100f;
    [SerializeField] private float damage = 30f;
    [SerializeField] private ParticleSystem muzzleFx;
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private float shotCooldown = 0.5f;

    [SerializeField] private Ammo ammoSlot;
    private bool canShoot = true;
  
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && canShoot == true)
        {            
            StartCoroutine(Shoot());
        }
        
    }

    IEnumerator Shoot()
    {
        canShoot = false;

        if(ammoSlot.GetCurrentAmmo() > 0)
        {
            ProcessRaycast();
            PlayMuzzleFlash();
            ammoSlot.ReduceCurrentAmmo();
        }
        
        yield return new WaitForSeconds(shotCooldown);

        canShoot = true;
    }

    private void PlayMuzzleFlash()
    {
        //파티클 이펙트 재생
        muzzleFx.Play();
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;
        
        if (Physics.Raycast(FPSCam.transform.position, FPSCam.transform.forward, out hit, range))
        {
            //TODO : 오브젝트 별로 Hit Effect 다르게 설정하기
            CreateHitEffect(hit);

            Debug.Log("I hit this thing : " + hit.transform.name);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();

            if (target != null)
            {
                target.TakeDamage(damage);                
            }

        }
        else
            return;
    }

    private void CreateHitEffect(RaycastHit hit)
    {
        GameObject effect = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(effect, 1);
    }
}
