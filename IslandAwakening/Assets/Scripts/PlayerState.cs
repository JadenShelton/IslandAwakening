using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public static PlayerState Instance { get; set; }
    //Player Health
    public float currentHealth;
    public float maxHealth;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
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

    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {   
        //health bar test
        /*
        if (Input.GetKeyDown(KeyCode.N))
        {
            currentHealth -= 10;
        }
        */
    }
      public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Max(currentHealth, 0f);
        Debug.Log($"Player took {amount} damage, now at {currentHealth}/{maxHealth}");
        if (currentHealth <= 0f)
        {
            // handle player death…
            Debug.Log("Player died!");
        }
    }
}
