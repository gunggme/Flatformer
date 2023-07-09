using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossHeathBar : MonoBehaviour
{
    public string bossName;
    
    public Slider healthSlider;
    public TMP_Text healthBarText;
    
    [SerializeField] Damageable bossDamageable;
    
    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        
        healthSlider = GetComponent<Slider>();

        bossDamageable.healthChanged.AddListener(OnPlayerHealthChanged);
        
        healthSlider.value = CalculateSliderPercentage(bossDamageable.Heath,  bossDamageable.MaxHealth);
        healthBarText.text = "HP " + bossDamageable.Heath + " / " + bossDamageable.MaxHealth;
        

    }

    private void OnDisable()
    {
        bossDamageable.healthChanged.RemoveListener(OnPlayerHealthChanged);
    }
    
    float CalculateSliderPercentage(float currentHealth, float maxHealth)
    {
        return currentHealth / maxHealth;
    }

    private void OnPlayerHealthChanged(int newHealth, int maxHealth)
    {
        healthSlider.value = CalculateSliderPercentage(newHealth,  maxHealth);
        healthBarText.text = "HP " + newHealth + " / " + maxHealth;
    }
}
