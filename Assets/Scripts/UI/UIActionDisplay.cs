using TMPro;
using TwitchIntegration;
using UnityEngine;

public class UIActionDisplay : MonoBehaviour
{
    public TextMeshProUGUI text;
    public TextMeshProUGUI compteur;
    public SO_Action _action;

    public void Init(SO_Action action)
    {
        text.text = action.actionName;
        _action = action;
    }

    public void AddVote()
    {
        TwitchUser user = new TwitchUser();
        user.userId = 0;
        TaskManager.Instance.OnTwitchMessageReceived(user, text.text);
    }

    public void UpdateCompteurVote()
    {
        int vote = TaskManager.Instance.GetActionVote(_action);
        if (vote != 0) { compteur.text = vote.ToString(); }
        
    }
}
