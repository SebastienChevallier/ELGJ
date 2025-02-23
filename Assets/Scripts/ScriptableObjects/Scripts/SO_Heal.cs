
using UnityEngine;

[CreateAssetMenu(fileName = "SO_heal", menuName = "Scriptable Objects/SO_Action/SO_heal")]
public class SO_heal : SO_Action
{
    public ParticleSystem healingParticles;
    public override void DoAction(object[] arg)
    {
        Instantiate(healingParticles);
        foreach (Hero hero in GameManager.Instance.heroes)
        {
            if (hero.TryGetComponent<IHealth>(out IHealth health))
            {
                health.UpdateHealth((int)arg[1]);
            }
        }
    }
}