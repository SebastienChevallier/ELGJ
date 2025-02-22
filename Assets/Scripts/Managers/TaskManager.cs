using System.Collections.Generic;
using System.Linq;
using TwitchIntegration;
using UnityEngine;

public class TaskManager : TwitchMonoBehaviour
{
    public static TaskManager Instance { get; private set; }    
    public bool canSelect;

    private Dictionary<int, string> userKeyPair = new Dictionary<int, string>();
    private Dictionary<SO_Action, int> actionChance = new Dictionary<SO_Action, int>();


    protected override void Awake()
    {
        base.Awake();
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
        TwitchManager.OnTwitchMessageReceived += OnTwitchMessageReceived;
    }

    private void Update()
    {
        Debug.Log("Listening Chat : " + canSelect);
    }

    private void OnTwitchMessageReceived(TwitchUser user, string message)
    {
        if (!canSelect) return;
        if (!userKeyPair.ContainsKey(user.userId))
        {
            userKeyPair.Add(user.userId, message);            
        }        
    }

    public void SelectAction(List<SO_Action> actions)
    {
        foreach (KeyValuePair<int, string> entry in userKeyPair)
        {
            CheckAction(actions, entry.Value);
            Debug.Log("Check action : " + entry.Value);
        }

        //GetBestAction();
    }

    public void CheckAction(List<SO_Action> actions, string value) 
    {
        foreach(SO_Action action in actions)
        {
            if(action.actionName == value)
            {
                if (actionChance.ContainsKey(action))
                {
                    actionChance[action]++;
                }
                else
                {
                    actionChance.Add(action, 1);
                }
            }
        }
    }

    public SO_Action GetBestAction()
    {
        int key = actionChance.Values.Max();
        SO_Action finalAction = actionChance.FirstOrDefault(x => x.Value == key).Key;
        Debug.Log("Best action : " + finalAction);
        return finalAction;
    }
}
