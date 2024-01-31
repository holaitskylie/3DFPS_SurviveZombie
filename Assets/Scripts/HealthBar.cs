using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private EnemyHealth enemy;
    [SerializeField] private Slider hpBar;
    [SerializeField] private Camera playerCam;

    private void Start()
    {
        enemy = GetComponentInParent<EnemyHealth>();
        hpBar = GetComponentInChildren<Slider>(); 
        hpBar.maxValue = enemy.startingHealth;

        playerCam = Camera.main;
        
    }

    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - playerCam.transform.position);
        UpdateHealthBar();
        
    }

    public void UpdateHealthBar()
    {
        hpBar.value = enemy.currentHealth;

        if(enemy.currentHealth <= 0)
        {
            hpBar.value = 0;
            //hpBar.enabled = false;
            hpBar.gameObject.SetActive(false);
        }
    }
}
