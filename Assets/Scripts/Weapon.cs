using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private PlayerHealth health;

    [Header("Gun Settings")]
    [SerializeField] Camera FPSCam;
    [SerializeField] private float range = 100f;
    [SerializeField] private float damage = 30f;
    [SerializeField] private ParticleSystem muzzleFx;
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private float shotCooldown = 0.5f;
    private bool canShoot = true;

    [Header("Ammo")]
    [SerializeField] private Ammo ammoSlot;
    [SerializeField] private AmmoType ammoType;

    private void OnEnable()
    {
        canShoot = true;
    }

    private void Start()
    {
         health = FindObjectOfType<PlayerHealth>();
    }

    void Update()
    {
        if (health.IsDead())
        {
            return;
        }

        if (Input.GetButtonDown("Fire1") && canShoot == true)
        {            
            StartCoroutine(Shoot());
        }
        
    }

    IEnumerator Shoot()
    {
        canShoot = false;

        //총마다 어떤 탄약을 사용할 것인지에 대한 설정을 가지고 있음
        //Gun 프리팹에 설정되어 있는 ammoType을 인자값으로 넣어
        //Player의 탄약 슬롯에서 현재 탄약의 수를 가져온다
        if(ammoSlot.GetCurrentAmmo(ammoType) > 0)
        {
            ProcessRaycast();
            PlayMuzzleFlash();
            ammoSlot.ReduceCurrentAmmo(ammoType);
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
