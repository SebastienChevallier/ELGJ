using System.Collections.Generic;
using TwitchIntegration;
using UnityEngine;

public class TaskManager : TwitchMonoBehaviour
{
    public static TaskManager Instance { get; private set; }    
    public bool canSelect;

    private Dictionary<int, string> userKeyPair = new Dictionary<int, string>();

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

    private void OnTwitchMessageReceived(TwitchUser user, string message)
    {
        if (!canSelect) return;
        if (!userKeyPair.ContainsKey(user.userId))
        {
            userKeyPair.Add(user.userId, message);
        }        
    }

    public void SelectAction()
    {

    }
}
