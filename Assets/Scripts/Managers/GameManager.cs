using UnityEngine;
using TwitchIntegration;
using Unity.VisualScripting;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public List<Entity> entities = new List<Entity>();
    public Queue<Entity> turnOrder = new Queue<Entity>();
    public Entity actualPlayable;

    public TextMeshProUGUI textMeshProUGUI;


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
        StartStep();
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
            Debug.Log(turnOrder.Count);
            actualPlayable = turnOrder.Dequeue();

            if(actualPlayable.TryGetComponent<IActions>(out IActions action))
            {
                StartCoroutine(StartVote(action));
            }
        }
        else
        {
            BuildQueue();
            StartStep();
        }
    }

    public void EndStep()
    {

    }

    public void EndFight()
    {

    }

    IEnumerator StartVote(IActions action)
    {
        float time = 0;
        textMeshProUGUI.text = time.ToString();

        while (time < 15)
        {
            time += Time.deltaTime;

            //Debug.Log(time);

            TaskManager.Instance.canSelect = true;
            textMeshProUGUI.text = time.ToString();

            yield return new WaitForEndOfFrame();
        }
        
        TaskManager.Instance.canSelect = false;

        TaskManager.Instance.SelectAction(action.GetActions());

        action.DoAction(TaskManager.Instance.GetBestAction());

        turnOrder.Clear();

    }

    
    
}
