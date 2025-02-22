using System.Collections.Generic;
using UnityEngine;

public class Hero : Entity, IActions
{
    public List<SO_Action> actions;
    public void DoAction(SO_Action action)
    {
        object[] arg = new object[0];
        action.DoAction(arg);
    }

    public List<SO_Action> GetActions()
    {
        return actions;
    }
}
