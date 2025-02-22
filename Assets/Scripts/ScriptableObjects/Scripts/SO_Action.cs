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



