using UnityEngine;

[CreateAssetMenu(fileName = "SO_Attack", menuName = "Scriptable Objects/SO_Action/SO_Attack")]
public class SO_attack : SO_Action
{
    public override void DoAction(object[] arg)
    {
        if (arg.Length > 0)
        {
            Mob MobHealth = (Mob)arg[0];
            if (MobHealth != null)
            {
                if (MobHealth.TryGetComponent<IHealth>(out IHealth health))
                {
                    health.UpdateHealth(-(int)arg[1]);
                }
            }
        }
    }
}

