using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : BaseManager
{
    public static GameManager Instance { get; private set; }

    public GameObject Player;
    private GameRepository gameRepository;
    private PlayerController playerController;
    private PlayerClothesData playerClothesData = new PlayerClothesData(0,0);

    public PlayerController PlayerController
    {
        get
        {
            if (playerController == null)
            {
                FindRepository();
                SpawnPlayer();
                FindPlayerController();
            }
            return playerController;
        }
    }
    protected override void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            base.Awake();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected override void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindRepository();
        FindPlayerController();
    }

    /// <summary>
    /// 현재 씬에서 GameRepository 찾기
    /// </summary>
    protected override void FindRepository()
    {
        gameRepository = FindObjectOfType<GameRepository>();
    }

    private void FindPlayerController()
    {
        if (gameRepository == null)
        {
            return;
        }

        playerController = Player.GetComponent<PlayerController>();
    }

    public MiniGameDataSO GetMiniGameInfo(ESceneType sceneType)
    {
        if (gameRepository != null)
        {
            return gameRepository.GetMiniGameInfo(sceneType);
        }
        return null;
    }

    private void SpawnPlayer()
    {
        GameObject playerPrefab = gameRepository.GetPlayerPrefab();
        Vector3 spawnPosition = gameRepository.GetSpawnPosition();

        Player = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
        Invoke("ApplySavedClothes", 0.1f);
    }

    private void ApplySavedClothes()
    {
        UIManager.Instance.clothesController.ChangeClothes(playerClothesData.clothID);
        UIManager.Instance.clothesController.ChangeColors(playerClothesData.colorID);
    }

    public PlayerClothesData GetPlayerClothes()
    {
        return playerClothesData;
    }
    public void SavePlayerClothes(int clothID, int colorID)
    {
        playerClothesData.clothID = clothID;
        playerClothesData.colorID = colorID;
    }
}
