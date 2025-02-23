using System.Collections;
using UnityEngine;

public class AnimationEventTrigger : MonoBehaviour
{
    public Mob mob;
    public Hero hero;

    #region Enemy
    public void MobBasicAttack()
    {
        mob.basicAttack();
    }

    public void MobAttackAOE()
    {
        mob.AttackAOE();
    }

    public void MobDie()
    {
        mob.Die();
    }
    #endregion

    public void HeroBasicAttack()
    {
        
    }

    public void HeroTaunt()
    {
        
    }

    public void HeroHeal()
    {
        
    }
}
