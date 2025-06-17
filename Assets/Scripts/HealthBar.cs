using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    
    public void UpdateHealthbar(int damage)
    {
        currentHealth = currentHealth - damage;
        
    }
}
