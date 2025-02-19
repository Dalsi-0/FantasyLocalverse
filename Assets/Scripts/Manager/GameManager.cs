using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject player;
    public PlayerController playerController;

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
        playerController = player.GetComponent<PlayerController>();
    }
}
