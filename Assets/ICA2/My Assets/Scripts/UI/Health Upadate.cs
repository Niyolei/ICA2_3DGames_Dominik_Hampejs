using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class HealthUpadate : MonoBehaviour
{
    public Image healthBar;
    public Image enemyHealthBar;
    
    public void UpdateHealth(float health)
    {
        healthBar.fillAmount = health;
    }
    
    public void UpdateEnemyHealth(float health)
    {
        enemyHealthBar.fillAmount = health;
    }
    
    
}
