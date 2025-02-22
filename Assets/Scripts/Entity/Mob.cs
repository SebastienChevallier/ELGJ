using System;
using System.Collections;
using UnityEngine;

public class Mob : Entity
{
    public Hero heroPriority;

    private Hero target;
    public void Attaque()
    {
        MobAction action = GetRandomEnumValue<MobAction>();
        
        switch(action)
        {
            case MobAction.Attack:

                break;
            case MobAction.AttackAOE:

                break;
        }

        Debug.Log("Orge Attaque ! ");
        StartCoroutine(DoEffect());
    }

    IEnumerator DoEffect()
    {
        //FX, Anim, Etc...
        yield return new WaitForSeconds(1);
        GameManager.Instance.EndStep();
        yield return null;
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
