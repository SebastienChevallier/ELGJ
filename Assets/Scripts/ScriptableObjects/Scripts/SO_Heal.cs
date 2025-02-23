
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

[CreateAssetMenu(fileName = "SO_heal", menuName = "Scriptable Objects/SO_Action/SO_heal")]
public class SO_heal : SO_Action
{
    public ParticleSystem healingParticles;
    public override void DoAction(object[] arg)
    {
        Hero entity = (Hero)arg[2];
        entity.animator.SetTrigger("Heal");

        Instantiate(healingParticles);

        foreach (Hero hero in GameManager.Instance.heroes)
        {
            if (hero.TryGetComponent<IHealth>(out IHealth health))
            {
                if (entity.bonusPowerCount > 0)
                {
                    health.UpdateHealth((int)arg[1] + entity.bonusPower);
                    entity.bonusPowerCount--;
                }
                else
                {
                    health.UpdateHealth((int)arg[1]);
                }
            }
        }
    }
}