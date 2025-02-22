using UnityEngine;

[CreateAssetMenu(fileName = "SO_Action", menuName = "Scriptable Objects/SO_Action")]
public class SO_Action : ScriptableObject
{
    public string actionName;
    public virtual void DoAction(object[] arg)
    {
        Debug.Log("Commande Test");
    }
}

[CreateAssetMenu(fileName = "SO_Attack", menuName = "Scriptable Objects/SO_Action/SO_Attack")]
public class SO_attack : SO_Action
{    
    public override void DoAction(object[] arg)
    {
        Debug.Log("Commande Attaque");
    }
}

[CreateAssetMenu(fileName = "SO_Attack", menuName = "Scriptable Objects/SO_Action/SO_Attack")]
public class SO_heal : SO_Action
{
    public override void DoAction(object[] arg)
    {
        Debug.Log("Commande heal");
    }
}

[CreateAssetMenu(fileName = "SO_Attack", menuName = "Scriptable Objects/SO_Action/SO_Attack")]
public class SO_taunt : SO_Action
{
    public override void DoAction(object[] arg)
    {
        Debug.Log("Commande taunt");
    }
}

