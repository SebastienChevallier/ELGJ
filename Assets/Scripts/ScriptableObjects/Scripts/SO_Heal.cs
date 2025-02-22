
using UnityEngine;

[CreateAssetMenu(fileName = "SO_heal", menuName = "Scriptable Objects/SO_Action/SO_heal")]
public class SO_heal : SO_Action
{
    public override void DoAction(object[] arg)
    {
        Debug.Log("Commande heal");
    }
}

