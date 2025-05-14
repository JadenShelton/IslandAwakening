using TMPro;
using UnityEngine;

public class PlayerCollectBanana : MonoBehaviour
{

    public int points = 0;
    
    private GUIStyle _scoreStyle;
    private const int Margin = 40;
    private const int Width   = 200;
    private const int Height  = 50;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
                // Lazily initialize your style
        if (_scoreStyle == null)
        {
            _scoreStyle = new GUIStyle(GUI.skin.label);
            _scoreStyle.fontSize = 36;                        
            _scoreStyle.alignment = TextAnchor.UpperRight;   
            _scoreStyle.normal.textColor = Color.white;      
        }

        // Compute a rect in the top-right:
        float x = Screen.width  - Width - Margin;
        float y = Margin;

        GUI.Label(
            new Rect(x, y, Width, Height),
            "Score: " + points,
            _scoreStyle
        );
    }
}
