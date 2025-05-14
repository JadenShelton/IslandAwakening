using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI finalScore;
   public void Setup(int Score) {
    gameObject.SetActive(true);

    gameObject.SetActive(true);

    // pause the game
    Time.timeScale = 0f;

    // unlock & show the cursor so the player can click
    Cursor.lockState = CursorLockMode.None;
    Cursor.visible   = true;

    finalScore.SetText("Final Score: {0}", Score);
   }

   public void PlayAgainButton() {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible   = false;
        SceneManager.LoadScene("TropicalEnvironment_Demo");
   }

   public void QuitButton() {
     Application.Quit();
     Debug.Log("Game closed");
   }
}
