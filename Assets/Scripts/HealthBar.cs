using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private EnemyHealth enemy;
    [SerializeField] private Slider hpBar;
    [SerializeField] private GameObject playerCam;

    private void Start()
    {
        hpBar.maxValue = 
    }

    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - playerCam.transform.position);
        
    }

    public void UpdateHealthMar(float maxHealth, float currentHealth)
    {

    }
}
