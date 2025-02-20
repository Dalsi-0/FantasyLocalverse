using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;


public class ClothesController : MonoBehaviour
{
    [SerializeField] private ClothesRepository clothesRepository;

    private PlayerController controller;


    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        controller = GameManager.Instance.PlayerController;
    }

    public void ChangeClothes(int clothID)
    {
        Sprite[] upper = clothesRepository.clothesDatas[clothID].spritesUpper;
        Sprite[] lower = clothesRepository.clothesDatas[clothID].spritesLower;

        for (int i = 0; i < upper.Length; i++)
        {
            controller.upperWearNomal[i].sprite = upper[i];
        }
        for (int i = 0; i < lower.Length; i++)
        {
            controller.lowerWearNomal[i].sprite = lower[i];
        }

        for (int i = 0; i < upper.Length; i++)
        {
            controller.upperWearRide[i].sprite = upper[i];
        }
    }

    public void ChangeColors(int colorId)
    {
        UnityEngine.Color color = UnityEngine.Color.white;
        switch (colorId)
        {
            case 0:
                color = UnityEngine.Color.white;
                break;

            case 1:
                color = UnityEngine.Color.red;
                break;

            case 2:
                color = UnityEngine.Color.green;
                break;

        }

        foreach (var item in controller.upperWearNomal)
        {
            item.color = color;
        }
        foreach (var item in controller.lowerWearNomal)
        {
            item.color = color;
        }

        foreach (var item in controller.upperWearRide)
        {
            item.color = color;
        }
    }
}
