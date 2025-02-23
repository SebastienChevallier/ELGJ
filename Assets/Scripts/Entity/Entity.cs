using UnityEngine;
using UnityEngine.UI;

public class Entity : MonoBehaviour, IHealth
{
    public EntityStats stats;
    public Slider healthSlider;
    public GameObject turnIndicator;

    public virtual void Start()
    {
        stats = Instantiate(stats);
        SetMaxHealth();
    }

    public virtual void SetMaxHealth()
    {
        stats.health = stats.maxHealth;
        if(healthSlider != null )
        {
            healthSlider.maxValue = stats.maxHealth;
            healthSlider.value = stats.health;
        }
    }   



    public virtual bool UpdateHealth(int amount)
    {
        //stats.health = Mathf.Clamp(stats.health + amount, 0, stats.maxHealth);
        stats.health = stats.health + amount;

        Debug.Log($"Inflige {amount} pour un une vie max de {stats.health}");

        if (healthSlider != null)
        {
            healthSlider.value = stats.health;
        }

        if (stats.health <= 0)
            Die();

        return amount > 0 ? true : false;
    }

    public virtual void Die()
    {
        Debug.Log("Grosse merde");
        //Destroy(gameObject, 3f);
    }
}
