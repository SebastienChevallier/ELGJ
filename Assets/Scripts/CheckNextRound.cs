using UnityEngine;

public class CheckNextRound : MonoBehaviour
{
    public int roundLeft = 4;
    void Start()
    {
        GameManager.Instance.onNextRound.AddListener(CheckRoundLeft);
    }

    public void CheckRoundLeft()
    {
        roundLeft--;
        if( roundLeft <= 0)
        {
            Destroy(gameObject);
        }
    }

}
