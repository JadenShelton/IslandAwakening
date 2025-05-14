using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager Instance { get; set; }

    public AudioSource grassWalk;

    public AudioSource bananaSFX;

    public AudioSource dayTimeBackgroundMusic;
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

    public void playSound(AudioSource soundToPlay)
    {
        if(!soundToPlay.isPlaying)
        {
            soundToPlay.Play();
        }
    }

    public void playSoundUncon(AudioSource soundToPlay)
    {
            soundToPlay.Play();

    }

}
