using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : Entity
{
    public int basicAttackDamage;
    public int AttackeAOEDamage;

    public Hero heroPriority;


    [Header("FX")]
    public ParticleSystem fireballParticles;
    public List<ParticleSystem> explosionParticles;
    public ParticleSystem dieParticles;
    public Transform particleInstatiatePos;
    public float tweenMoveDuration;

    public void Attaque()
    {
        StartCoroutine(WaitToAttack());
    }

    IEnumerator WaitToAttack()
    {
        yield return new WaitForSeconds(4f);
        MobAction action = GetRandomEnumValue<MobAction>();

        switch (action)
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

        yield return DOTween.Sequence()
            .Append(fireballParticles.transform.DOMove(target.transform.position + Vector3.up, tweenMoveDuration).SetEase(Ease.Linear))
            .WaitForCompletion();


        target.UpdateHealth(-basicAttackDamage);
        yield return new WaitForSeconds(1);
        GameManager.Instance.EndStep();
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
        yield return new WaitForSeconds(.2f);
        foreach(ParticleSystem ps in explosionParticles)
        {
            ps.gameObject.SetActive(true);
        }
        yield return new WaitForSeconds(1.3f);

        foreach (ParticleSystem ps in explosionParticles)
        {
            ps.gameObject.SetActive(false);
        }
        GameManager.Instance.EndStep();
    }

    public override void Die()
    {
        base.Die();
        dieParticles.gameObject.SetActive(true);
    }

    public override void Hit()
    {
        base.Hit();

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
