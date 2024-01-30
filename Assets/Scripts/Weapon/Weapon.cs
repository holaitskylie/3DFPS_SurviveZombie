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
    private AudioSource gunAudioPlayer; //�� �Ҹ� �����
    [SerializeField] private AudioClip shotClip; //�߻� �Ҹ�
    //[SerializeField] private AudioClip reloadClip; //������ �Ҹ�

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

        //�Ѹ��� � ź���� ����� �������� ���� ������ ������ ����
        //Gun �����տ� �����Ǿ� �ִ� ammoType�� ���ڰ����� �־�
        //Player�� ź�� ���Կ��� ���� ź���� ���� �����´�
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
        //�ѱ� ȭ�� ����Ʈ ���
        muzzleFx.Play();

        //ź�� ���� ����Ʈ ���
        //shellFX.Play();

        //�Ѱ� �Ҹ� ���
        gunAudioPlayer.PlayOneShot(shotClip);
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;

        //ź���� ���� ��
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
