using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] PlayerHealth player;
    private Slider hpSlider;
    private Text hpText;

    // Start is called before the first frame update
    void Start()
    {
        if(player == null)
            player = FindObjectOfType<PlayerHealth>();

        hpSlider = GetComponentInChildren<Slider>();
        hpSlider.maxValue = player.startingHealth;
        
        hpText = GetComponentInChildren<Text>();        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthUI();
        
    }

    private void UpdateHealthUI()
    {
        hpSlider.value = player.currentHealth;

        if(player.currentHealth > player.startingHealth )
            player.currentHealth = player.startingHealth;

        hpText.text = player.currentHealth.ToString() + "/" + player.startingHealth.ToString();
    }
}
