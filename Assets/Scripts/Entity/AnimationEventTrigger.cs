using System.Collections;
using UnityEngine;

public class AnimationEventTrigger : MonoBehaviour
{
    public Mob mob;

    public void MobBasicAttack()
    {
        mob.basicAttack();
    }

    public void MobAttackAOE()
    {
        mob.AttackAOE();
    }

/*    private void Start()
    {
        StartCoroutine(test());
    }

    IEnumerator test()
    {
        yield return new WaitForSeconds(2f);
        mob.animator.SetTrigger("Attack");
    }*/
}
