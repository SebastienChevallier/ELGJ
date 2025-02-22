using UnityEngine;

[CreateAssetMenu(fileName = "SO_taunt", menuName = "Scriptable Objects/SO_Action/SO_taunt")]
public class SO_taunt : SO_Action
{
    public override void DoAction(object[] arg)
    {
        if (arg.Length > 0)
        {
            Hero entity = (Hero)arg[3];
            Mob mob = (Mob)arg[0];
            if (entity != null)
            {
                mob.heroPriority = entity;
            }
        }
    }
}


