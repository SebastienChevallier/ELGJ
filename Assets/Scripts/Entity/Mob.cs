using System.Collections;
using UnityEngine;

public class Mob : Entity
{
    public void Attaque()
    {
        Debug.Log("Orge Attaque ! ");
        StartCoroutine(DoEffect());
    }

    IEnumerator DoEffect()
    {
        //FX, Anim, Etc...
        yield return new WaitForSeconds(1);
        GameManager.Instance.EndStep();
        yield return null;
    }
}
