using UnityEngine;

public class CamShakeManager : MonoBehaviour
{
    public static CamShakeManager Instance { get; private set; }

    private Vector3 _OriginalPos;
    public float shakeFrequency;
    public float shakeDuration = 0f;
    public float decreaseFactor = 1.0f;

    protected void Awake()
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
        _OriginalPos = transform.position;
    }

    private void Update()
    {
        if (shakeDuration > 0)
        {
            transform.position = _OriginalPos + Random.insideUnitSphere * shakeFrequency;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            transform.position = _OriginalPos;
        }
    }

    public void DoShake(float duration, float frequency, float decrease)
    {
        shakeDuration = duration; //shakeFrequency = frequency; shakeDuration = decrease;
    }
    
}
