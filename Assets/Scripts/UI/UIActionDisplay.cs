using TMPro;
using TwitchIntegration;
using UnityEngine;

public class UIActionDisplay : MonoBehaviour
{
    public TextMeshProUGUI text;

    public void Init(SO_Action action)
    {
        text.text = action.actionName;
    }

    public void AddVote()
    {
        TwitchUser user = new TwitchUser();
        user.userId = 0;
        TaskManager.Instance.OnTwitchMessageReceived(user, text.text);
    }
}
