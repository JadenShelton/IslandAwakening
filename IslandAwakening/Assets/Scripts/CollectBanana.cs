using UnityEngine;

public class CollectBanana : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            other.GetComponent<PlayerCollectBanana>().points++;
            SoundManager.Instance.playSoundUncon(SoundManager.Instance.bananaSFX);
            Destroy(gameObject);
            //SoundManager.Instance.grassWalk.Stop();
        }
    }
}
