using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "StatData", order = 1)]
public class EntityStats : ScriptableObject
{
    public int health;
    public int maxHealth;
    public int power;
}