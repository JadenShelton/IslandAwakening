using UnityEngine;

public class TimeManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static TimeManager Instance { get; set; }
    void Awake()
    {
        if (Instance != null && Instance != this) 
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        
    }

    public int dayInGame = 1;

    public void TriggerNextDay()
    {
        dayInGame += 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
