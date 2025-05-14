using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private Slider slider;
    public Text healthCounter;
    public GameObject playerState;
    private float currentHealth, maxHealth;
    void Awake()
    {
        slider = GetComponent<Slider>();

    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = playerState.GetComponent<PlayerState>().currentHealth;
        maxHealth = playerState.GetComponent<PlayerState>().maxHealth;

        float fillValue = currentHealth / maxHealth; // 0 - 1
        slider.value = fillValue;

        healthCounter.text = currentHealth + "/" +maxHealth;

    }
}
