using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_buff", menuName = "Scriptable Objects/SO_Action/SO_buff")]
public class SO_Buff : SO_Action
{
    [Range(0, 1)]
    public float percentageDamageIncrease;
    public List<ParticleSystem> buffParticles;
    public override void DoAction(object[] arg)
    {
        Hero entity = (Hero)arg[2];
        entity.animator.SetTrigger("Buff");

        foreach (var particle in buffParticles) {
            Instantiate(particle);
        }

        foreach (Hero hero in GameManager.Instance.heroes ) {
            hero.bonusPowerCount = 1;
            hero.bonusPower = (int) (hero.stats.power * percentageDamageIncrease);
        }
    }
}
