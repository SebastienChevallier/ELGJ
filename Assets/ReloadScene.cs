using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    public void OnClickRetry()
    {
        SceneManager.LoadScene(1);
    }
}
