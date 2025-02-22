using UnityEngine;
using TwitchIntegration;
using Unity.VisualScripting;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public List<Entity> entities = new List<Entity>();
    public Queue<Entity> turnOrder = new Queue<Entity>();
    public Entity actualPlayable;

    public Mob mob;
    public List<Hero> heroes = new List<Hero>();

    public TextMeshProUGUI textMeshProUGUI;
    public GameObject UIActionParent;
    public GameObject UIActionPrefab;


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
        foreach (Entity ent in entities)
        {
            if(ent.TryGetComponent<Mob>(out Mob _mob))
            {
                mob = _mob;
            }

            if (ent.TryGetComponent<Hero>(out Hero hero))
            {
                heroes.Add(hero);
            }
        }


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
                //Afficher les options sur l'ecran
                DisplayActions(action);

                StartCoroutine(StartVote(action));
            }

            if (actualPlayable.TryGetComponent<Mob>(out Mob Mob))
            {
                Mob.Attaque();
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
        Debug.Log("Begin End Step");
        foreach(UIActionDisplay display in uIActionDisplays)
        {
            Destroy(display.gameObject);
        }
        uIActionDisplays.Clear();
        Debug.ClearDeveloperConsole();
        StartStep();
    }

    public void EndFight()
    {

    }

    IEnumerator StartVote(IActions action)
    {
        float time = 0;
        textMeshProUGUI.text = time.ToString();

        while (time < 20)
        {
            time += Time.deltaTime;

            //Debug.Log(time);

            TaskManager.Instance.canSelect = true;
            textMeshProUGUI.text = ((int)time).ToString();

            yield return new WaitForEndOfFrame();
        }
        
        TaskManager.Instance.canSelect = false;

        TaskManager.Instance.SelectAction(action.GetActions());

        action.DoAction(TaskManager.Instance.GetBestAction());

        EndStep();
        //turnOrder.Clear();
    }

    private List<UIActionDisplay> uIActionDisplays = new List<UIActionDisplay>();
    private void DisplayActions(IActions action)
    {
        foreach(SO_Action act in action.GetActions())
        {
            GameObject GO =  Instantiate(UIActionPrefab, UIActionParent.transform);
            UIActionDisplay display =  GO.GetComponent<UIActionDisplay>();
            display.Init(act);
            uIActionDisplays.Add(display);
        }
    }
}
