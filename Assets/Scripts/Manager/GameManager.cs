using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : BaseManager
{
    public static GameManager Instance { get; private set; }

    private GameRepository gameRepository;
    private PlayerController playerController;

    public PlayerController PlayerController
    {
        get
        {
            if (playerController == null)
            {
                FindRepository();
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

    private void OnEnable()
    {
        FindRepository();
        FindPlayerController();
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

        GameObject player = gameRepository.GetPlayer();
        playerController = player.GetComponent<PlayerController>();
    }

    public MiniGameDataSO GetMiniGameInfo(ESceneType sceneType)
    {
        if (gameRepository != null)
        {
            return gameRepository.GetMiniGameInfo(sceneType);
        }
        return null;
    }
}
