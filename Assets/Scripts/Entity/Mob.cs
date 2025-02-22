using System;
using System.Collections;
using UnityEngine;

public class Mob : Entity
{
    public int basicAttackDamage;
    public int AttackeAOEDamage;

    public Hero heroPriority;
    public Animator animator;

    public void Attaque()
    {
        MobAction action = GetRandomEnumValue<MobAction>();
        
        switch(action)
        {
            case MobAction.Attack:

                animator.Play("AttackBasic");
                break;
            case MobAction.AttackAOE:
                animator.Play("AttackAOE");
                break;
        }
    }

    IEnumerator BasicAttack() 
    {
        Hero target;
        if (heroPriority)
        {
            target = heroPriority;
        } else
        {
            target = GameManager.Instance.heroes[UnityEngine.Random.Range(0, GameManager.Instance.heroes.Count)];
        }
        target.UpdateHealth(-basicAttackDamage);
        yield return new WaitForSeconds(1);
        GameManager.Instance.EndStep();
    }

    IEnumerator AttackAOE()
    {
        foreach(Hero hero in GameManager.Instance.heroes)
        {
            hero.UpdateHealth(-AttackeAOEDamage);
        }
        yield return new WaitForSeconds(1);
        GameManager.Instance.EndStep();
    }
    public T GetRandomEnumValue<T>()
    {
        Array values = Enum.GetValues(typeof(T));
        return (T)values.GetValue(UnityEngine.Random.Range(0, values.Length));
    }
}

public enum MobAction
{
    Attack,
    AttackAOE,
}
