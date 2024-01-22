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
  
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        
    }

    private void Shoot()
    {
        ProcessRaycast();
        PlayMuzzleFlash();

    }

    private void PlayMuzzleFlash()
    {
        //��ƼŬ ����Ʈ ���
        muzzleFx.Play();
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;
        
        if (Physics.Raycast(FPSCam.transform.position, FPSCam.transform.forward, out hit, range))
        {
            //TODO : ������Ʈ ���� Hit Effect �ٸ��� �����ϱ�
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
