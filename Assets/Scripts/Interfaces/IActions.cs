using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public interface IActions
{
    public void DoAction(SO_Action action);
    public List<SO_Action> GetActions();
}
