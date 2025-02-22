using JetBrains.Annotations;
using UnityEngine;

public interface IHealth
{
    public void SetMaxHealth();

    public void SetMaxMana();

    public bool UpdateMana(int amount);

    public bool UpdateHealth(int amount);

    public void Die();
}
