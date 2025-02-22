using UnityEngine;

public class EntityAttack : Entity, IActions
{
    public Entity entityTarget;
    public override void Start()
    {
        base.Start();
    }

    #region Health&Mana
    public override void SetMaxHealth()
    {
        base.SetMaxHealth();
    }

    public override void SetMaxMana()
    {
        base.SetMaxMana();
    }
    public override bool UpdateMana(int amount)
    {
        return base.UpdateMana(amount);
    }

    public override bool UpdateHealth(int amount)
    {
        return base.UpdateHealth(amount);
    }

    public override void Die()
    {
        base.Die();
    }
    #endregion

    #region Action
    public virtual void Attack()
    {
        int damage = StaticStats.baseAttackDamage * (stats.force / StaticStats.statsIncreaseDamage);
        entityTarget.UpdateHealth(-damage);
    }

    public virtual bool CastSpellAttack()
    {
        if (stats.mana <= 0)
            return false;

        int damage = StaticStats.spellAttackDamage * (stats.intelligence / StaticStats.statsIncreaseDamage);
        UpdateMana(StaticStats.spellAttackCost);
        entityTarget.UpdateHealth(-damage);

        return true;
    }

    public virtual bool CastSpellHeal()
    {
        if (stats.mana <= 0)
            return false;

        int heal = StaticStats.spellHealAmount * (stats.intelligence / 2);
        UpdateMana(StaticStats.spellHealCost);
        entityTarget.UpdateHealth(heal);

        return true;
    }

    public virtual void NinjaMode()
    {
        print("NINJAAAAAA !!!!!");
    }

    #endregion
}
