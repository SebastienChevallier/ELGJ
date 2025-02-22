using UnityEngine;
using TwitchIntegration;
using Unity.VisualScripting;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public List<Entity> entities = new List<Entity>();
    private Queue<Entity> turnOrder;
    private Entity actualPlayable;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        
    }

    public void BuildQueue()
    {
        turnOrder.Clear();

        foreach (var entity in entities)
        {
            if (!turnOrder.Contains(entity))
            {
                turnOrder.Enqueue(entity);
            }
        }
    }

    public void StartStep()
    {
        if(turnOrder.Count > 0)
        {
            actualPlayable = turnOrder.Dequeue();
        }
        else
        {
            BuildQueue();
        }
    }

    public void EndStep()
    {

    }

    public void EndFight()
    {

    }
    
}
