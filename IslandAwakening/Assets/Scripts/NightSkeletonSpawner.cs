using UnityEngine;
using UnityEngine.AI;

public class NightSkeletonSpawner : MonoBehaviour
{
    [Header("References")]
    // new! drag your Player here in the Inspector
    public Transform player;

    [Tooltip("The skeleton prefab with your SkeletonAI etc.")]
    public GameObject skeletonPrefab;

    [Header("When to spawn")]
    public int nightStartHour = 18;

    [Header("Spawn Volume")]
    public Transform spawnCenter;
    public float spawnRadius = 20f;

    [Header("Difficulty Scaling")]
    public int baseSpawnCount = 2;
    public int perDayIncrement = 1;

    bool hasSpawnedThisNight = false;
    DayNightSystem dayNight;
    TimeManager timeMan;

    void Awake()
    {
        dayNight = FindObjectOfType<DayNightSystem>();
        timeMan = TimeManager.Instance;
    }

    void Update()
    {
        if (dayNight.currentHour >= nightStartHour && !hasSpawnedThisNight)
        {
            SpawnNightWave();
            hasSpawnedThisNight = true;
        }
        else if (dayNight.currentHour < nightStartHour)
        {
            hasSpawnedThisNight = false;
        }
    }

    void SpawnNightWave()
    {
        int wave = baseSpawnCount + (timeMan.dayInGame - 1) * perDayIncrement;
        for (int i = 0; i < wave; i++)
        {
            // pick a NavMesh‐valid spawn point
            Vector3 rnd = Random.insideUnitSphere * spawnRadius + spawnCenter.position;
            NavMeshHit hit;
            Vector3 spawnPos = NavMesh.SamplePosition(rnd, out hit, spawnRadius, NavMesh.AllAreas)
                               ? hit.position
                               : spawnCenter.position;

            // instantiate
            GameObject go = Instantiate(skeletonPrefab, spawnPos, Quaternion.identity);

            // **assign the player to the new skeleton’s AI**
            SkeletonAI ai = go.GetComponent<SkeletonAI>();
            ai.player = player;
        }
    }
}
