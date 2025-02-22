using TMPro;
using UnityEngine;

public class UIActionDisplay : MonoBehaviour
{
    public TextMeshProUGUI text;

    public void Init(SO_Action action)
    {
        text.text = action.name;
    }
}
