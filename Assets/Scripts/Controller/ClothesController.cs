using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ClothesController : MonoBehaviour
{
    [SerializeField] private ClothesRepository clothesRepository;

    private PlayerController controller;

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

    public void ChangeColors(UnityEngine.Color color)
    {
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
