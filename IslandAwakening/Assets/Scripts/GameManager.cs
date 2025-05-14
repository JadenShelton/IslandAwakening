using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Tooltip("Your PlayerCollectBanana component")]
    public PlayerCollectBanana playerCollector;

    [Tooltip("Your PlayerState component")]
    public PlayerState       playerState;

    [Tooltip("The GameOver UI panel (with GameOver script)")]
    public GameOver          gameOverUI;

    [Tooltip("How many bananas to collect to win")]
    public int               winPoints = 30;

    bool _gameOver;

    void Start()
    {
        // make sure your GameOver panel is hidden at launch
        gameOverUI.gameObject.SetActive(false);
    }

    void Update()
    {
        if (_gameOver) return;

        if (playerCollector.points >= winPoints ||
            playerState.currentHealth <= 0f)
        {
            _gameOver = true;
            gameOverUI.Setup(playerCollector.points);
            Time.timeScale = 0f;   // optional: pause the game
        }
    }
}
