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

        //�Ѹ��� � ź���� ����� �������� ���� ������ ������ ����
        //Gun �����տ� �����Ǿ� �ִ� ammoType�� ���ڰ����� �־�
        //Player�� ź�� ���Կ��� ���� ź���� ���� �����´�
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
