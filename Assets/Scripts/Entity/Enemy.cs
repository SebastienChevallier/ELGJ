using UnityEngine;

public class Enemy : EntityAttack
{
    [Header("ForceEnemy")]
    [Range(0, 1)]
    public float healChance;

    public override void Start()
    {
        base.Start();
        entityTarget = Hero.instance;
    }
    public void AttackIA()
    {
        if (stats.mana == 0)
        {
            Attack();
            return;
        }

        float attackResult = Random.Range(0, 1);
        if (attackResult <= healChance)
        {
            CastSpellHeal();
            return;
        }

        float total = stats.force + stats.intelligence;
        float pForce = stats.force / total;
        float pIntelligence = stats.intelligence / total;

        float rand = Random.value;

        // Choix de l'attaque en fonction des probabilités
        if (rand < pForce)
        {
            Attack();
        }
        else
        {
            CastSpellAttack();
        }
    }

    public override void Attack()
    {
        base.Attack();
    }
    public override bool CastSpellAttack()
    {
        return base.CastSpellAttack();
    }

    public override bool CastSpellHeal()
    {
        return base.CastSpellHeal();
    }

    public override void Die()
    {
        base.Die();

    }
}
