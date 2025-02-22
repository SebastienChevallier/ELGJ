using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    public List<List<Enemy>> allEnemy;

    public Transform enemyPos;
    public int roundIndex;
    public Enemy currentEnemy;

    [Header("a voir si vous aimez")]
    public bool isDifferenteStats;
    public List<List<EntityStats>> enemyStats;
    private void Awake()
    {
        instance = this;
    }
    
    public void pullEnemy()
    {
        currentEnemy = Instantiate(allEnemy[roundIndex][Random.Range(0, allEnemy[roundIndex].Count)], enemyPos);
        if (isDifferenteStats)
        {
            currentEnemy.stats = enemyStats[roundIndex][Random.Range(0, allEnemy[roundIndex].Count)];
        }
    }

    public void NextRound()
    {
        roundIndex++;
        Destroy(currentEnemy.gameObject);
        pullEnemy();
    }
}
