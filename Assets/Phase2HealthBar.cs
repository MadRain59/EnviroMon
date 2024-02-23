using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Phase2HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    Damageable playerDamagable;

    private void Awake()
    {
        GameObject phase2 = GameObject.FindGameObjectWithTag("Phase2");
        playerDamagable = phase2.GetComponent<Damageable>();
    }
    // Start is called before the first frame update
    void Start()
    {
        healthSlider.value = CalculateSliderPercentage(playerDamagable.Health, playerDamagable.MaxHealth);
    }

    private void OnEnable()
    {
        playerDamagable.healthChanged.AddListener(OnPlayerHealthChanged);
    }
    // Update is called once per frame
    private void OnDisable()
    {
        playerDamagable.healthChanged.RemoveListener(OnPlayerHealthChanged);
    }

    private float CalculateSliderPercentage(float currentHealth, float maxHealth)
    {
        return currentHealth / maxHealth;
    }

    private void OnPlayerHealthChanged(int newHealth, int maxHealth)
    {
        healthSlider.value = CalculateSliderPercentage(newHealth, maxHealth);
    }
}