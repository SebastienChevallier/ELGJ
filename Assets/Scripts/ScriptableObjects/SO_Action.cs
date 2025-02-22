using UnityEngine;

[CreateAssetMenu(fileName = "SO_Action", menuName = "Scriptable Objects/SO_Action")]
public class SO_Action : ScriptableObject
{
    public string actionName;
    public virtual void DoAction()
    {
        Debug.Log("Commande Test");
    }
}

[CreateAssetMenu(fileName = "SO_Attack", menuName = "Scriptable Objects/SO_Action/SO_Attack")]
public class SO_attack : SO_Action
{
    
    public override void DoAction()
    {
        Debug.Log("Commande Attaque");
    }
}

