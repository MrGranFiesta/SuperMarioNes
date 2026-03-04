using System.Linq;
using UnityEngine;

public class PlayerLifecycleManager : MonoBehaviour
{
    public static PlayerLifecycleManager Instance { get; private set; }

    private GameObject playerPrefab;
    [SerializeField] private Level GameOverScene;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        MainClass.CustomEvents.OnPlayerDeath.AddListener(OnPlayerDeath);
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        playerPrefab = ResourceManager.GetPlayer();
        PlayerController player = FindAnyObjectByType<PlayerController>();
        Vector3 spawnerPoint = transform.position;
        Vector3 positionSpawner = transform.position;

        SpawnPointLocation? SpawnPointLocation = GetSpawnPointLocation();

        if (SpawnPointLocation != null)
        {
            SpawnPointController[] spawnPoints = FindObjectsOfType<SpawnPointController>();
            SpawnPointController spawnPointController = spawnPoints.Where(x => SpawnPointLocation == x.SpawnPointLocation).FirstOrDefault();

            if (spawnPointController != null)
            {
                positionSpawner = spawnPointController.transform.position;
            }
        }
        
        if (player == null)
        {
            Instantiate(playerPrefab, positionSpawner, Quaternion.identity);
        }
        else
        {
            player.transform.position = positionSpawner;
        }
    }

    private SpawnPointLocation? GetSpawnPointLocation()
    {
        if (MainClass.Player.SpawnPointTemporaly != null)
        {
            return MainClass.Player.GetSpawnPointTemporalyAndRemove();
        }
        else if (MainClass.Player.CheckPoint != null)
        {
            return MainClass.Player.CheckPoint;
        }
        return null;
    }
    private void OnPlayerDeath()
    {
        GameOverScene.LoadLevel();
    }
}
