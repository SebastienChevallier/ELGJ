using UnityEngine;

[CreateAssetMenu(fileName = "SO_taunt", menuName = "Scriptable Objects/SO_Action/SO_taunt")]
public class SO_taunt : SO_Action
{
    public override void DoAction(object[] arg)
    {
        Debug.Log("Commande taunt");
    }
}


