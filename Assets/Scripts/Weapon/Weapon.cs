using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{   
    private JoystickController joystick;

    [Header("Gun Settings")]
    [SerializeField] Camera FPSCam;
    [SerializeField] private float range = 100f;
    [SerializeField] private float damage = 30f;
    //[SerializeField] private ParticleSystem muzzleFx;
    [SerializeField] private float shotCooldown = 0.5f;
    private bool canShoot = true;

    [Header("Sounds")]
    private AudioSource gunAudioPlayer; //총 소리 재생기
    [SerializeField] private AudioClip shotClip; //발사 소리

    [Header("Ammo")]
    [SerializeField] private Ammo ammoSlot;
    [SerializeField] private AmmoType ammoType;

    [Header("UI")]
    public Image crossHair;
    private Color originColor;

    private void OnEnable()
    {
        canShoot = true;
    }

    private void Start()
    {
        StartCoroutine("GameManagerInitializtion");
    }

    IEnumerator GameManagerInitializtion()
    {
        while (GameManager.instance == null)
            yield return null;

        gunAudioPlayer = GetComponent<AudioSource>();       
        joystick = FindObjectOfType<JoystickController>();

        originColor = crossHair.color;

    }
       

    void Update()
    {
        if (GameManager.instance.isDialogueActive || GameManager.instance.isGameOver)
            return;       

       /* if (Input.GetButtonDown("Fire1") && canShoot == true)
        {            
            StartCoroutine(Shoot());
        }*/

        if(joystick.shootToggle == true && canShoot == true)
            StartCoroutine(Shoot());     
        
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
        joystick.shootToggle = false;
    }

    private void PlayShotEffect()
    {
        //총구 화염 이펙트 재생
        //muzzleFx.Play();       

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
            if (hit.collider.CompareTag("Player")) 
                return;           
                
            IDamageable target = hit.collider.GetComponent<IDamageable>();

            if (target != null)
            {
                target.OnDamage(damage, hit.point, hit.normal);
                crossHair.color = Color.yellow;
            }
            else
            {
                crossHair.color = originColor;
                return;
            }
                         
            
            hitPosition = hit.point;            

        }
        else
            return;
    }
       
}
