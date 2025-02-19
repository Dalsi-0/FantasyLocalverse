using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject player;
    public MiniGameRepository miniGameRepository;
    private PlayerController playerController;
    public PlayerController PlayerController
    {
        get
        {
            if (playerController == null && player != null)
            {
                playerController = player.GetComponent<PlayerController>();
            }
            return playerController;
        }
    }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        InitSetting();
    }

    void InitSetting()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }
}
