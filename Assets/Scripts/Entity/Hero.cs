using System.Collections.Generic;
using UnityEngine;

public class Hero : Entity, IActions
{
    public List<SO_Action> actions;
    public float timeBeforeExecuteAction;

    public int bonusPower;
    public int bonusPowerCount;

    public void Awake()
    {
        for(int i = 0; i < actions.Count; i++)
        {
            actions[i] = Instantiate(actions[i]);
        }
    }

    public void DoAction(SO_Action action)
    {
        object[] arg = new object[4];
        arg[0] = GameManager.Instance.mob;
        arg[1] = stats.power;
        arg[2] = this;
        arg[3] = timeBeforeExecuteAction;
        action.DoAction(arg);
    }

    public List<SO_Action> GetActions()
    {
        return actions;
    }

    public override void Die()
    {
        base.Die();
        GameManager.Instance.heroes.Remove(this);
    }
}
