using UnityEngine;
using TwitchIntegration;

using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public List<Entity> entities = new List<Entity>();
    public Queue<Entity> turnOrder = new Queue<Entity>();
    public Entity actualPlayable;

    public Mob mob;
    public List<Hero> heroes = new List<Hero>();
    public int TurnNumber = 0;

    public Slider timerSlider;
    public GameObject UIActionParent;
    public GameObject UIActionPrefab;
    public TextMeshProUGUI actualPlayableText;
    public List<GameObject> allCanvas = new List<GameObject>();
    public UnityEvent onNextRound;
    public GameObject losePanel, winPanel;
    public bool isEnd;


    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        isEnd = false;
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

        

        Invoke("StartStep", 4f);
        //StartStep();
    }

    public void BuildQueue()
    {
        if (isEnd)
            return;

        TurnNumber++;
        turnOrder.Clear();
        string order = "";
        int i = 0;

        foreach (Entity entity in entities.OrderByDescending(e => e.stats.speed))
        {
            if (!turnOrder.Contains(entity))
            {
                i++;

                turnOrder.Enqueue(entity);
                order += $"N°{i} : {entity.name} with {entity.stats.speed} speed\n";
            }
        }

        //Debug.Log(order);
    }

    public void UnQueueEntity(Entity entity)
    {
        if (isEnd)
            return;

        if (turnOrder.Contains(entity))
        {
            Entity[] ents = turnOrder.ToArray();
            turnOrder.Clear();

            if (entities.Contains(entity))
            {
                entities.Remove(entity);
            }

            foreach (Entity ent in ents)
            {
                if(ent != entity)
                {
                    turnOrder.Enqueue(ent);
                }
                
            }
        }
    }

    public void StartStep()
    {
        if (isEnd)
            return;

        foreach (GameObject go in allCanvas)
        {
            if (!go.activeSelf) go.SetActive(true);
        }

        if (turnOrder.Count > 0)
        {
            //Debug.Log(turnOrder.Count);

            if (actualPlayable)
            {
                actualPlayable.turnIndicator.SetActive(false);
            }

            actualPlayable = turnOrder.Dequeue();
            actualPlayable.turnIndicator.SetActive(true);

            actualPlayableText.text = actualPlayable.entityName;


            if (actualPlayable.TryGetComponent<IActions>(out IActions action))
            {
                //Afficher les options sur l'ecran
                DisplayActions(action);
                
                StartCoroutine(StartVote(action));
            }

            if (actualPlayable.TryGetComponent<Mob>(out Mob Mob))
            {
                Mob.Attaque();
            }
            onNextRound.Invoke();
        }
        else
        {
            BuildQueue();
            StartStep();
        }
    }

    public void EndStep()
    {
        if (isEnd)
            return;

        //Debug.Log("Begin End Step");
        foreach (UIActionDisplay display in uIActionDisplays)
        {
            Destroy(display.gameObject);
        }
        uIActionDisplays.Clear();        
        StartStep();
    }

    public void EndFight()
    {

    }

    IEnumerator StartVote(IActions action)
    {
        if (isEnd)
            yield break;

        TaskManager.Instance.ClearChat();

        float time = 0f;
        timerSlider.value = time;
        //int i = 0;

        while (time < 20f)
        {
            time += Time.deltaTime;
            //Debug.Log(time);

            TaskManager.Instance.canSelect = true;
            timerSlider.value = time;

            /*if (time > i)
            {
                //TaskManager.Instance.SelectAction(action.GetActions());
                //TaskManager.Instance.userKeyPair.Clear();
                i++;                
                UpdateUIButtons();
            }*/

            yield return new WaitForEndOfFrame();
        }

        TaskManager.Instance.canSelect = false;

        TaskManager.Instance.SelectAction(action.GetActions());

        SO_Action act = TaskManager.Instance.GetBestAction();        

        if (act != null) 
        {
            action.DoAction(act);            
        }

        EndStep();
    }

    private void UpdateUIButtons()
    {
        foreach(UIActionDisplay uIActionDisplay in uIActionDisplays)
        {
            uIActionDisplay.UpdateCompteurVote();
        }
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

    public void Win()
    {
        isEnd = true;
        turnOrder.Clear();
        winPanel.SetActive(true);
        foreach(Entity en in entities)
        {
            UnQueueEntity(en);
        }
    }

    public void Loose()
    {
        isEnd = true;
        turnOrder.Clear();
        losePanel.SetActive(true);
        foreach (Entity en in entities)
        {
            UnQueueEntity(en);
        }
    }

    public void OnClickRetry()
    {
        SceneManager.LoadScene(1);
    }

    public void OnClickRestart()
    {
        SceneManager.LoadScene(0);
    }
}
