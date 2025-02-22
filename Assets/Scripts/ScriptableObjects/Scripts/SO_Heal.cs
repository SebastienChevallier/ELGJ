
using UnityEngine;

[CreateAssetMenu(fileName = "SO_heal", menuName = "Scriptable Objects/SO_Action/SO_heal")]
public class SO_heal : SO_Action
{
    public override void DoAction(object[] arg)
    {
        if (arg.Length > 0)
        {
            Entity entity = (Entity)arg[2];
            if (entity != null)
            {
                if (entity.TryGetComponent<IHealth>(out IHealth health))
                {
                    health.UpdateHealth((int)arg[1]);
                }
            }
        }
    }
}

