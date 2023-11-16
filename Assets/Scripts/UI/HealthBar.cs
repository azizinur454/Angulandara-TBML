using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public TMP_Text healthBarText;
    public Slider healthSlider;

    Damage playerDamage;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if(player == null)
        {
            Debug.Log("No player object in the scene");
        }
        playerDamage = player.GetComponent<Damage>();
    }

    private void Start()
    {
        healthSlider.value = CalculateSliderPercentage(playerDamage.Health, playerDamage.MaxHealth);
        healthBarText.text = "HP " + playerDamage.Health + " / " + playerDamage.MaxHealth;
    }

    private void OnEnable()
    {
        playerDamage.healthChanged.AddListener(OnPlayerHealthChanged);
    }
    private void OnDisable()
    {
        playerDamage.healthChanged.RemoveListener(OnPlayerHealthChanged);
    }
    private float CalculateSliderPercentage(float currentHealth, float maxHealth)
    {
        return currentHealth / maxHealth;
    }

    private void OnPlayerHealthChanged(int newHealth, int maxHealth)
    {
        healthSlider.value = CalculateSliderPercentage(newHealth, maxHealth);
        healthBarText.text = "HP " + newHealth + " / " + maxHealth;
    }
}
