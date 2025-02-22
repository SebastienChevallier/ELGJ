using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class Mob : Entity
{
    public int basicAttackDamage;
    public int AttackeAOEDamage;

    public Hero heroPriority;
    public Animator animator;


    [Header("FX")]
    public ParticleSystem fireballParticles;
    public Transform particleInstatiatePos;
    public float tweenMoveDuration;

    public void Attaque()
    {
        MobAction action = GetRandomEnumValue<MobAction>();
        
        switch(action)
        {
            case MobAction.Attack:
                animator.SetTrigger("Attack");
                break;
            case MobAction.AttackAOE:
                animator.SetTrigger("AttackAOE");
                break;
        }
    }
    public void basicAttack()
    {
        StartCoroutine(BasicAttackCoroutine());
    }
    IEnumerator BasicAttackCoroutine() 
    {
        Hero target;
        if (heroPriority)
        {
            target = heroPriority;
        } else
        {
            target = GameManager.Instance.heroes[UnityEngine.Random.Range(0, GameManager.Instance.heroes.Count)];
        }

        fireballParticles.gameObject.SetActive(true);
        fireballParticles.transform.position = particleInstatiatePos.position;

        DOTween.Sequence()
            .Append(fireballParticles.transform.DOMove(target.transform.position, tweenMoveDuration).SetEase(Ease.Linear))
            .WaitForCompletion();


        yield return null;
        target.UpdateHealth(-basicAttackDamage);
        yield return new WaitForSeconds(1);
        GameManager.Instance.EndStep();
        yield return new WaitForSeconds(1);
        fireballParticles.gameObject.SetActive(false);


    }

    public void AttackAOE()
    {
        StartCoroutine(AttackAOECoroutine());
    }

    IEnumerator AttackAOECoroutine()
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
