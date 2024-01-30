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
    //[SerializeField] private ParticleSystem shellFX;

    [SerializeField] private float shotCooldown = 0.5f;
    private bool canShoot = true;

    [Header("Sounds")]
    private AudioSource gunAudioPlayer; //총 소리 재생기
    [SerializeField] private AudioClip shotClip; //발사 소리
    //[SerializeField] private AudioClip reloadClip; //재장전 소리

    [Header("Ammo")]
    [SerializeField] private Ammo ammoSlot;
    [SerializeField] private AmmoType ammoType;

    private void OnEnable()
    {
        canShoot = true;
    }

    private void Start()
    {
        gunAudioPlayer = GetComponent<AudioSource>();

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
            PlayShotEffect();
            ammoSlot.ReduceCurrentAmmo(ammoType);
        }
        
        yield return new WaitForSeconds(shotCooldown);

        canShoot = true;
    }

    private void PlayShotEffect()
    {
        //총구 화염 이펙트 재생
        muzzleFx.Play();

        //탄피 배출 이펙트 재생
        //shellFX.Play();

        //총격 소리 재생
        gunAudioPlayer.PlayOneShot(shotClip);
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;

        //탄알이 맞은 곳
        Vector3 hitPosition = Vector3.zero;
        
        if (Physics.Raycast(FPSCam.transform.position, FPSCam.transform.forward, out hit, range))
        {
            Debug.Log("I hit this thing : " + hit.transform.name);

            if (hit.collider.CompareTag("Player")) return;           
                
            IDamageable target = hit.collider.GetComponent<IDamageable>();

            if (target != null)
            {
                target.OnDamage(damage, hit.point, hit.normal);
            }
            else
            {
                Debug.Log("Target does not implement IDamageable: " + hit.collider.gameObject.name);
                return;
            }


            hitPosition = hit.point;            

        }
        else
            return;
    }
       
}
