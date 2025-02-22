using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "EnemyData", order = 1)]
public class EntityStats : ScriptableObject
{
    public int health;
    public int maxHealth;
    public int mana;
    public int maxMana;

    public int force;
    public int intelligence;
}
