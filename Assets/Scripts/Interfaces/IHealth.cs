using UnityEngine;

public interface IHealth
{
    public void SetMaxHealth();
    public bool UpdateHealth(int amount);

    public void Die();
}
