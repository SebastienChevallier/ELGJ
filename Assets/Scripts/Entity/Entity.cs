using UnityEngine;

public class Entity : MonoBehaviour, IHealth
{
    public EntityStats stats;

    public virtual void Start()
    {
        stats = Instantiate(stats);
        SetMaxHealth();
    }

    #region Health&Mana
    public virtual void SetMaxHealth()
    {
        stats.health = stats.maxHealth;
    }
    public virtual void SetMaxMana()
    {
        stats.mana = stats.maxMana;
    }

    public virtual bool UpdateMana(int amount)
    {
        stats.mana = Mathf.Clamp(stats.mana + amount, 0, stats.maxMana);

        return stats.mana > 0 ? true : false;
    }

    public virtual bool UpdateHealth(int amount)
    {
        stats.health = Mathf.Clamp(stats.health +amount,0,stats.maxHealth);

        if (stats.health <= 0)
            Die();
        
        return amount > 0 ? true : false;
    }

    public virtual void Die()
    {
        Debug.Log("Grosse merde");
        Destroy(gameObject, 3f);
    }
    #endregion

}
